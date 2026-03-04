using System.Collections;
using System.Collections.Generic;
using Team3.Characters;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer.EnemyCamp
{
    public class EnemyCamp : NetworkBehaviour
    {
        [SerializeField] private EnemyWaveSpawner enemySpawner;
        [SerializeField] private float resetTimer;
        [SerializeField] private SphereCollider sphereCollider;

        private HashSet<ulong> playerInCampById;

        public Vector3 Center => sphereCollider.center;
        public float Radius => sphereCollider.radius;
        public float SpawnRadius => enemySpawner.SpawnRadius;
        public float GroundCheckLength => enemySpawner.GroundCheckLength;

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                enemySpawner.gameObject.SetActive(false);
                return;
            }

            playerInCampById = new HashSet<ulong>();

            if (enemySpawner.SpawnRadius > sphereCollider.radius)
            {
                Debug.LogWarning($"{nameof(enemySpawner.SpawnRadius)} is greater than {nameof(sphereCollider.radius)}. The {nameof(sphereCollider.radius)} should be greater");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NetworkCharacter player))
            {
                PlayerEnteredServerRpc(player.OwnerClientId);
            }
        }

        [ServerRpc]
        private void PlayerEnteredServerRpc(ulong playerId)
        {
            playerInCampById.Add(playerId);

            if (playerInCampById.Count > 0)
            {
                StartCoroutine(enemySpawner.SpawnWaves());
            }

            StopCoroutine(ResetCamp());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out NetworkCharacter player))
            {
                PlayerExitServerRpc(player.OwnerClientId);
            }
        }

        [ServerRpc]
        private void PlayerExitServerRpc(ulong playerId)
        {
            playerInCampById.Remove(playerId);

            if (playerInCampById.Count == 0)
            {
                if (enemySpawner.IsFinished)
                { return; }

                StartCoroutine(ResetCamp());
            }
        }

        private IEnumerator ResetCamp()
        {
            yield return new WaitForSeconds(resetTimer);

            // enemySpawner.Reset();
            // StopCoroutine(enemySpawner.SpawnWaves());
        }
    }
}
