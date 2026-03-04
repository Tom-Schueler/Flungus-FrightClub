using System.Collections.Generic;
using Team3.Combat;
using Unity.Netcode;
using UnityEngine;

namespace Team3.MOBA
{
    public class CollectablePerk : NetworkBehaviour
    {
        [SerializeField]
        List<SOCombatCards> perklist = new();
        int perkID;

        [SerializeField]
        MeshRenderer card;
        [SerializeField]
        MeshRenderer bubble;

        [SerializeField]
        Material Redbubble;
        [SerializeField]
        Material RedCard;

        [SerializeField]
        Material Goldbubble;
        [SerializeField]
        Material GoldCard;

        [SerializeField]
        Material Greenbubble;
        [SerializeField]
        Material GreenCard;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            SpawnEffectClientRpc(69);
            SOCombatCards newCard= perklist[Random.Range(0, perklist.Count)];
            perkID = newCard.ID;
            CardAlignment cardAlignment = newCard.Alignment;

            switch (cardAlignment)
            {
                case CardAlignment.none:
                    break;
                case CardAlignment.Heaven:
                    card.material = GoldCard;
                    bubble.material = Goldbubble;
                    break;
                case CardAlignment.Hell:
                    card.material = RedCard;
                    bubble.material = Redbubble;
                    break;
                case CardAlignment.Purgatory:
                    card.material = GreenCard;
                    bubble.material = Greenbubble;
                    break;
                default:
                    break;
            }


        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.TryGetComponent<NetworkObject>(out NetworkObject nobj))
                {
                    SpawnEffectClientRpc(70);
                    ulong noID = nobj.OwnerClientId;
                    AddPerkClientRpc(noID, perkID);
                    gameObject.GetComponent<NetworkObject>().Despawn(true);
                }
            }
        }

        [ClientRpc]
        void AddPerkClientRpc(ulong noID,int perkID)
        {
            if(NetworkManager.Singleton.LocalClientId == noID)
            {
                PlayerRegistry.GetStats(noID).AddPerk(perkID);
            }
        }

        [ClientRpc]
        void SpawnEffectClientRpc(int id)
        {
            Instantiate(PerkDatabase.Instance.GetVFXByID(id).VFX,transform.position, Quaternion.identity);
        }
    }




}