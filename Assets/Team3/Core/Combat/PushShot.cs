using Team3.Characters;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;



namespace Team3.Combat
{
    

    [CreateAssetMenu(fileName = "PushShot", menuName = "Team3/Combat/Perk/PushShot")]
    public class PushShot : SOProjectilePerk
    {
        public float ExplosionForce = 100;
        public override void AdjustStats(ProjectileCore projectile)
        {

        }

        public override void TriggerPerk(ProjectileCore projectile = null, ulong ownerID = 420, 
                                        Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), 
                                        NetworkObjectReference hitRef = default, Collision collision = null)
        {

            // First, resolve the reference to a real NetworkObject
            if (!hitRef.TryGet(out NetworkObject hitObject))
                return;

            // Now try to get the component from the resolved object
            if (!hitObject.TryGetComponent<NetworkCharacter>(out var move))
                return;

            ulong targetClientId = hitObject.OwnerClientId;

            // Finally call the ClientRpc on the correct client only
            move.ApplyExplosionForceClientRpc(position, ExplosionForce, targetClientId);
        }





        public override void ServerTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1, NetworkObjectReference hitRef = default)
        {
            // First, resolve the reference to a real NetworkObject
            if (!hitRef.TryGet(out NetworkObject hitObject))
                return;

            // Now try to get the component from the resolved object
            if (!hitObject.TryGetComponent<NetworkCharacter>(out var move))
                return;

            ulong targetClientId = hitObject.OwnerClientId;

            // Finally call the ClientRpc on the correct client only
            move.ApplyExplosionForceClientRpc(position, ExplosionForce, targetClientId);
        }

        public override void ClientTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1)
        {
        }

        public override void PerkUpdate()
        {

        }

        public override void PerkApply(Vector3 position, Vector3 rotation, NetworkObjectReference hitRef = default)
        {

        }

        public override void TriggerDeathPerk(ProjectileCore projectile = null, ulong OwnerID = 420, Vector3 position = default, Vector3 rotation = default, NetworkObjectReference hitRef = default, Collision collision = null)
        {
        }
    }
}