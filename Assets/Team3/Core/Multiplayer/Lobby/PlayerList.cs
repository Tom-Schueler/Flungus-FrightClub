using Team3.Multiplayer.Player;
using UnityEngine;

namespace Team3.Multiplayer.Lobby
{
    public class PlayerList : MonoBehaviour
    {
        [SerializeField] private PlayerInfoDisplay playerInfoPrefab;

        private void OnEnable()
        {
            MultiplayerPlayer.OnPlayerJoinedLobby += OnPlayerJoined;
        }

        private void OnDisable()
        {
            MultiplayerPlayer.OnPlayerJoinedLobby -= OnPlayerJoined;
        }

        private void OnPlayerJoined(MultiplayerPlayer player)
        {
            PlayerInfoDisplay playerInfoDisplay = Instantiate(playerInfoPrefab, transform);
            playerInfoDisplay.Initialize(player);
        }
    }
}
