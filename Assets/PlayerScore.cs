using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviourPunCallbacks
{
    TextMeshProUGUI scoreText;

    void Start()
    {
        if (!photonView.IsMine) return;

        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        int currentScore = 0;

        if (photonView.Owner.CustomProperties.ContainsKey("score"))
        {
            currentScore = (int)photonView.Owner.CustomProperties["score"];
        }

        Hashtable hash = new Hashtable();
        hash["score"] = currentScore + amount;

        photonView.Owner.SetCustomProperties(hash);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!photonView.IsMine) return;
        if (targetPlayer != photonView.Owner) return;

        if (changedProps.ContainsKey("score"))
        {
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText == null) return;

        int score = 0;

        if (photonView.Owner.CustomProperties.ContainsKey("score"))
        {
            score = (int)photonView.Owner.CustomProperties["score"];
        }

        scoreText.text = "Score: " + score;
    }
}