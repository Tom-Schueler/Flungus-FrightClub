using Team3.Characters;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer
{
    public class GameManager : MonoBehaviour
    {

        private void OnEnable()
        {
            PlayerStats.OnPlayerSpawn += PlayerSpawned;

        }

        private void OnDisable()
        {
            PlayerStats.OnPlayerSpawn -= PlayerSpawned;
        }

        public void PlayerSpawned(ulong playerID)
        {
           

            PlayerRegistry.GetStats(playerID).currentHealth.OnValueChanged += TrackPlayerHealth;

            void TrackPlayerHealth(float old, float current)
            {
               

            }
        }
    }
}