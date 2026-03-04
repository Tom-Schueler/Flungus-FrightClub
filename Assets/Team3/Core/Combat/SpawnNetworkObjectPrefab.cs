using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using Team3.Weapons;

namespace Team3.Combat {

    [CreateAssetMenu(fileName = "SpawnNetworkObjectPrefab", menuName = "Team3/Combat/Perk/SpawnNetworkObjectPrefab")]
    public class SpawnNetworkObjectPrefab : SOProjectilePerk
    {
        [SerializeField] private GameObject prefab;
  
        public override void AdjustStats(ProjectileCore projectile)
        {
            
        }

        public override void TriggerPerk(ProjectileCore projectile = null, ulong OwnerID = 420, Vector3 position = default, Vector3 rotation = default, NetworkObjectReference hitRef = default, Collision collision = null)
        {
            if (projectile != null)
            {
                position = projectile.transform.position;
            }

            var no = Instantiate(prefab, position, Quaternion.LookRotation(Vector3.forward)).GetComponent<NetworkObject>();
            no.Spawn(); ;
        }

        public override void ServerTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1, NetworkObjectReference hitRef = default )
        {
        }

        public override void ClientTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1)
        {
        }

        public override void PerkApply(Vector3 position, Vector3 rotation, NetworkObjectReference hitRef = default)
        {
        }

        public override void PerkUpdate()
        {
            
        }

        public override void TriggerDeathPerk(ProjectileCore projectile = null, ulong OwnerID = 420, Vector3 position = default, Vector3 rotation = default, NetworkObjectReference hitRef = default, Collision collision = null)
        {
        }
    }
}