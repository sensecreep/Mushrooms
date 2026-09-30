using Photon.Pun;
using UnityEngine;

public class PlayerCamera : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
        }
    }
}