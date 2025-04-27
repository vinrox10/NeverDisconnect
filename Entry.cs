using BepInEx;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[BepInPlugin("yourname.reconnectmod", "ReconnectMod", "0.1.0")]
public class Entry : BaseUnityPlugin
{
    void Awake()
    {
        // Keep players/room alive for 180s after disconnect
        var options = new RoomOptions {
            PlayerTtl    = 180_000,
            EmptyRoomTtl = 180_000
        };
        PhotonNetwork.JoinOrCreateRoom("ReconnectRoom", options, TypedLobby.Default);

        new GameObject("ReconnectHandler").AddComponent<ReconnectHandler>();
    }
}
