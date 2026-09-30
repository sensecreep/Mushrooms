using Photon.Pun;
using UnityEngine;

public class ItemPickup : MonoBehaviourPun
{
    bool playerNearby = false;
    GameObject player;

    void Update()
    {
        if (!playerNearby) return;
        if (player == null) return;

        PhotonView playerView = player.GetComponent<PhotonView>();

        if (!playerView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            photonView.RPC("PickupItem", RpcTarget.MasterClient, playerView.ViewID);
        }
    }

    [PunRPC]
    void PickupItem(int playerViewID)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        PhotonView playerView = PhotonView.Find(playerViewID);

        PlayerScore score = playerView.GetComponent<PlayerScore>();
        score.AddScore(1);

        PhotonNetwork.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PhotonView view = other.GetComponent<PhotonView>();
        if (!view.IsMine) return;

        playerNearby = true;
        player = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PhotonView view = other.GetComponent<PhotonView>();
        if (!view.IsMine) return;

        playerNearby = false;
        player = null;
    }
}

/*
public class ItemPickup : MonoBehaviourPun
{
    bool playerNearby = false;
    GameObject player;

    void Update()
    {
        if (!playerNearby) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PhotonView playerView = player.GetComponent<PhotonView>();

            if (!playerView.IsMine) return;

            PlayerScore score = player.GetComponent<PlayerScore>();
            score.AddScore(1);

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = true;
        player = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerNearby = false;
        player = null;
    }
} */