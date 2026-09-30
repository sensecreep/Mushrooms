using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviourPun
{
    public float roundTime = 60f;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;

    bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        if (!PhotonNetwork.IsMasterClient) return;

        roundTime -= Time.deltaTime;

        photonView.RPC("UpdateTimer", RpcTarget.All, roundTime);

        if (roundTime <= 0)
        {
            gameEnded = true;
            photonView.RPC("EndGame", RpcTarget.All);
        }
    }

    [PunRPC]
    void UpdateTimer(float time)
    {
        if (time < 0) time = 0;

        timerText.text = "Time: " + Mathf.Ceil(time);
    }

    [PunRPC]
    void EndGame()
    {
        timerText.text = "Time: 0";

        Player winner = null;
        int bestScore = -1;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            int score = 0;

            if (player.CustomProperties.ContainsKey("score"))
            {
                score = (int)player.CustomProperties["score"];
            }

            if (score > bestScore)
            {
                bestScore = score;
                winner = player;
            }
        }

        if (winner != null)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = "WINNER\n" + winner.NickName + "\nScore: " + bestScore;
        }

        Time.timeScale = 0f;
    }
}