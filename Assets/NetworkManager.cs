using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("Connected to server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "Room1",
            new RoomOptions { MaxPlayers = 5 },
            TypedLobby.Default
        );
    }

    public override void OnJoinedRoom()
    {
        int playerNumber = PhotonNetwork.PlayerList.Length;

        PhotonNetwork.NickName = "Player " + playerNumber;

        Debug.Log("Joined room");

        Vector3 spawnPos = new Vector3(
            Random.Range(-4, 4),
            1,
            Random.Range(-4, 4)
        );

        PhotonNetwork.Instantiate(
            "PlayerPrefab",
            spawnPos,
            Quaternion.identity
        );
    }
}