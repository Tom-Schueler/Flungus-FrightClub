using Team3.Multiplayer;
using TMPro;
using UnityEngine;

namespace Team3.UserInterface
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeDisplay;

        private void Start()
        {
            MatchCycle.OnUpdateTimeDisplay += UpdateTime;
        }

        private void UpdateTime(int timeLeft)
        {
            timeDisplay.text = timeLeft.ToString();
        }
    }
}
