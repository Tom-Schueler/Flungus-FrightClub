using System.Linq;
using Team3.UserInterface;
using UnityEngine;

namespace Team3.Multiplayer
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private GameObject contentField;
        [SerializeField] private PlayerScoreDisplay playerScoreDisplay;

        private void OnEnable()
        {
            MatchCycle.OnUpdateScoreboard += UpdateScoreboard;
        }


        private void OnDisable()
        {
            MatchCycle.OnUpdateScoreboard -= UpdateScoreboard;
        }

        private void UpdateScoreboard(ScoreboardInfo[] scoreboardInfos)
        {
            var orderedInfos = scoreboardInfos.OrderByDescending(info => info.Points);

            foreach (ScoreboardInfo scoreboardInfo in orderedInfos)
            {
                PlayerScoreDisplay display = Instantiate(playerScoreDisplay, contentField.transform);
                display.Initiate(scoreboardInfo.Name, scoreboardInfo.Points);
            }
        }
    }
}
