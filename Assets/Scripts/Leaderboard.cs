using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "e5c7793237b3ba42af66bf7612a0d90255856692ae87e501ab8d21a598e3f49c";

    /*
     * Secret Key:
     * 483cedd7a0e224376b92b083508fe4f07d96b3aa252d9ab07f9d67066e7d4d05afe319dc1da1873d6da2dbdfb74a52a601b0712cfe484ec0a05e42feee1b084c9c59222678a264364d268ee0f1fe7952e57160106983e65a07482c23c6f4d6dcd7fe69c19fd607219468039db66d9179ecf0f9c6d0065d55ab9f1e55078662bd
     */

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
