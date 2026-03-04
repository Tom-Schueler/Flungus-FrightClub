using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using Team3.Weapons;
using Team3.Combat;
using UnityEngine.ProBuilder;

namespace Team3.MOBA {

    [CreateAssetMenu(fileName = "GhostProjectile", menuName = "Team3/Combat/Perk/GhostProjectile")]
    public class DroneShot : SOProjectilePerk
    {
        [SerializeField] private GameObject ClientProjectile;





        public override void AdjustStats(ProjectileCore projectile)
        {
            
        }

        public override void TriggerPerk(ProjectileCore projectile = null, ulong ownerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null)
        {
        }


        public override void TriggerDeathPerk(ProjectileCore projectile = null, ulong ownerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null)
        {
        }

        public override void ServerTrigger(Vector3 position, Vector3 rotation,ulong clientID, int id = -1, NetworkObjectReference hitRef = default)
        {
        }

        public override void ClientTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1)
        {
            var clientProjectile = Instantiate(ClientProjectile, position, Quaternion.LookRotation(Vector3.forward));      
        }

        public override void PerkUpdate()
        {
            
        }

        public override void PerkApply(Vector3 position, Vector3 rotation, NetworkObjectReference hitRef = default)
        {
         
        }
    }
}