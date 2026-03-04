using TMPro;
using UnityEngine;

namespace Team3.UserInterface
{
    public class PlayerScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerName;
        [SerializeField] private TMP_Text points;

        public void Initiate(string name, int points)
        {
            playerName.text = name;
            this.points.text = points.ToString();
        }
    }
}
