using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ReconnectHandler : MonoBehaviourPunCallbacks
{
    public GameObject panel;    // assign in inspector or instantiate
    public Text reconnectText;  // assign in inspector

    const float GRACE = 180f;
    bool reconnecting;

    void Start() => panel.SetActive(false);

    public override void OnDisconnected(DisconnectCause cause)
    {
        reconnecting = true;
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        float t = GRACE;
        panel.SetActive(true);
        while (t > 0f && reconnecting)
        {
            reconnectText.text = $"Reconnectingâ€¦ {Mathf.Ceil(t)}s";
            PhotonNetwork.ReconnectAndRejoin();
            yield return new WaitForSeconds(1f);
            t -= 1f;
        }
        panel.SetActive(false);
        if (!PhotonNetwork.IsConnectedAndReady)
            Debug.LogWarning("ReconnectMod: failed to reconnect within 180s.");
    }

    public override void OnConnectedToMaster()
    {
        reconnecting = false;
        panel.SetActive(false);
        Debug.Log("ReconnectMod: reconnected.");
    }
}
