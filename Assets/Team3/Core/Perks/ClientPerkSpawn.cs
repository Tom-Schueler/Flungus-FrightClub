using Team3.Combat;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Combat
{
    public class ClientPerkSpawn : NetworkBehaviour
    {
        public SOProjectilePerk perk;
        public ulong OwnerID = 420;


        public void Start()
        {
            perk.ServerTrigger(transform.position, transform.forward,OwnerID);
            TriggerServerPerkServerRpc(perk.ID);
            GetComponent<NetworkObject>().Despawn(true);
        }


        [ServerRpc(RequireOwnership = false)]
        public void TriggerServerPerkServerRpc(int perkid)
        {
            TriggerClientPerkClientRpc(perkid);
        }


        [ClientRpc(RequireOwnership = false)]
        public void TriggerClientPerkClientRpc(int perkid)
        {
            PerkDatabase.Instance.GetPerkByID(perkid).ClientTrigger(transform.position, transform.forward, OwnerID,-1);
        }
    }
}