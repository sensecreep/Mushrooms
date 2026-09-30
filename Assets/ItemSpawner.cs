using Photon.Pun;
using UnityEngine;

public class ItemSpawner : MonoBehaviourPunCallbacks
{
    public int itemCount = 20;

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        for (int i = 0; i < itemCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-12, 12),
                1f,
                Random.Range(-12, 12)
            );

            PhotonNetwork.Instantiate("ItemPrefab", pos, Quaternion.identity);
        }
    }
}