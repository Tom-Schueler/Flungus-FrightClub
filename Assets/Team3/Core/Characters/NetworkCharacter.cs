using Unity.Netcode;
using UnityEngine;

namespace Team3.Characters
{
    public class NetworkCharacter : NetworkBehaviour
    {
        [SerializeField] private GameObject povCamera;
        [SerializeField] private GameObject cineBrain;
        [SerializeField] private GameObject characterMovement;

        [SerializeField] private Rigidbody body;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                povCamera.SetActive(false);
                cineBrain.SetActive(false);
                characterMovement.SetActive(false);
            }
        }

        [ClientRpc]
        public void ApplyExplosionForceClientRpc(Vector3 forceDirection, float force, ulong targetClientId)
        {
            if (NetworkManager.Singleton.LocalClientId != targetClientId)
                return;

            // Apply the force locally
            body.AddForce((transform.position - forceDirection).normalized * force, ForceMode.Impulse);

            print(targetClientId + " FORCE IS APPLIED LIKE CRAZY");
        }

        [ClientRpc]
        public void ApplyImplosionForceClientRpc(Vector3 forcePosition, float force, ulong targetClientId)
        {
            if (NetworkManager.Singleton.LocalClientId != targetClientId)
                return;

            // Apply the force locally
            body.AddForce((forcePosition - transform.position).normalized * force, ForceMode.Impulse);

            print(targetClientId + " FORCE IS APPLIED LIKE CRAZY");
        }
    }
}
