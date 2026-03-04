using System.Collections;
using Team3.Multiplayer.Player;
using TMPro;
using UnityEngine;

namespace Team3.Multiplayer.Lobby
{
    public class LatencyDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text latencyLabel;

        private void OnEnable()
        {
            MultiplayerPlayer.OnUpdateLatencyDisplay += UpdateLatencyDisplay;
        }

        private void OnDisable()
        {
            MultiplayerPlayer.OnUpdateLatencyDisplay -= UpdateLatencyDisplay;
        }

        private void UpdateLatencyDisplay(ulong rtt)
        {
            latencyLabel.text = $"{rtt:0.##}";
        }
    }
}
