using Photon.Pun;
using UnityEngine;

public class LoadingScreen : MonoBehaviourPunCallbacks
{
    public GameObject loadingScreen;

    void Start()
    {
        loadingScreen.SetActive(true);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loadingScreen.SetActive(false);
    }
}