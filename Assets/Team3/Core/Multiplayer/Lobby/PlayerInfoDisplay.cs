using System;
using System.Collections;
using Team3.Multiplayer.Player;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Team3.Multiplayer.Lobby
{
    public class PlayerInfoDisplay : MonoBehaviour
    {
        [SerializeField] private Image playerInfo;
        [SerializeField] private GameObject border;
        [SerializeField, Range(0.01f, 5)] private float updateFrequency;
        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private Image ghost;
        [SerializeField] private Sprite[] sprites;

        private MultiplayerPlayer player;

        public void Initialize(MultiplayerPlayer player)
        {
            this.player = player;
            player.OnDisconnect += RemoveDisplay;

            nameLabel.text = player.playerName.Value.ToString();
            ghost.sprite = sprites[player.OwnerClientId];

            player.playerName.OnValueChanged += (FixedString32Bytes oldString, FixedString32Bytes newString) =>
            {
                nameLabel.text = newString.ToString();
            };

            if (player.IsOwner && player.IsOwnedByHost)
            {
                playerInfo.color = Color.blue;
            }
            else if (player.IsOwnedByHost)
            {
                playerInfo.color = Color.red;
            }
            else if (player.IsOwner)
            {
                playerInfo.color = Color.green;
            }
            else
            {
                playerInfo.color = Color.magenta;
            }

            StartCoroutine(UpdateDisplay());
        }

        private IEnumerator UpdateDisplay()
        {
            while (true)
            {
                if (!player.IsOwnedByHost && ReadyTracker.IsClientReady(player.OwnerClientId))
                {
                    border.SetActive(true);
                }
                else
                {
                    border.SetActive(false);
                }

                yield return new WaitForSeconds(updateFrequency);
            }
        }

        private void RemoveDisplay()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            player.OnDisconnect -= RemoveDisplay;
        }
    }
}
