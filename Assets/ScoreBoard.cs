using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    TextMeshProUGUI scoreboardText;

    void Start()
    {
        scoreboardText = GameObject.Find("ScoreboardText").GetComponent<TextMeshProUGUI>();
        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        string text = "Scoreboard:\n";

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            int score = 0;

            if (player.CustomProperties.ContainsKey("score"))
            {
                score = (int)player.CustomProperties["score"];
            }

            text += player.NickName + " : " + score + "\n";
        }

        scoreboardText.text = text;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("score"))
        {
            UpdateScoreboard();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateScoreboard();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateScoreboard();
    }
}