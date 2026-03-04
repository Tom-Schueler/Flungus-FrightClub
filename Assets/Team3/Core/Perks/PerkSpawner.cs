using Team3.Combat;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Combat
{
    public class PerkSpawner : NetworkBehaviour
    {
        public SOProjectilePerk perk;
        public ulong OwnerID;
        public int vfxid = -1;

        public void Start()
        {
            if (perk.VFX != null)
            {
                vfxid = perk.VFX.ID;
            }
            perk.ServerTrigger(transform.position,transform.forward, OwnerID); // Will call into SpawnFireBall
            TriggerServerPerkServerRpc(perk.ID, vfxid);
            GetComponent<NetworkObject>().Despawn();
        }


        [ServerRpc(RequireOwnership = false)]
        public void TriggerServerPerkServerRpc(int perkid, int vfxid)
        {
            TriggerClientPerkClientRpc(perkid, vfxid);
        }

        [ClientRpc(RequireOwnership = false)]
        public void TriggerClientPerkClientRpc(int perkid, int vfxid)
        {

            PerkDatabase.Instance.GetPerkByID(perkid).ClientTrigger(transform.position,transform.forward, OwnerID, vfxid); // Visual-only;

        }





















        [ClientRpc]
        public void ApplyExplosionForceClientRpc(ulong targetClientId, Vector3 explosionPos, float force, float radius)
        {
            if (NetworkManager.Singleton.LocalClientId != targetClientId)
                return;

            if (TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddExplosionForce(force, explosionPos, radius);
            }
        }
    }
}