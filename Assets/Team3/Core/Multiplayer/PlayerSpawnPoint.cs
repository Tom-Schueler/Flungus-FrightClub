using System.Collections.Generic;
using UnityEngine;

namespace Team3.Multiplayer
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, Min(0)] private float debugRadius;
        [SerializeField] private Color debugColor;

        private void OnDrawGizmos()
        {
            Gizmos.color = debugColor;
            Gizmos.DrawSphere(transform.position, debugRadius);
        }
#endif

        private static readonly List<PlayerSpawnPoint> playerSpawnPoints = new List<PlayerSpawnPoint>();
        public static IReadOnlyList<PlayerSpawnPoint> AllPoints => playerSpawnPoints;

        private void Awake()
        {
            // maybe despawn if not server

            playerSpawnPoints.Add(this);
        }

        private void OnDestroy()
        {
            playerSpawnPoints.Remove(this);
        }
    }
}
