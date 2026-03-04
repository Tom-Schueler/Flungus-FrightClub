using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using Team3.Weapons;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


namespace Team3.Combat {

    [CreateAssetMenu(fileName = "ClientPrefabSpawner", menuName = "Team3/Combat/Perk/ClientPrefabSpawner")]
    public class DroneShot : SOProjectilePerk
    {
        //[SerializeField] private GameObject networkSpawner;
        [SerializeField] private GameObject gameObjectToSpawnOnClient;
        [SerializeField] private GameObject gameObjectToSpawnOnServer;





        public override void AdjustStats(ProjectileCore projectile)
        {
            
        }

        public override void TriggerPerk(ProjectileCore projectile = null, ulong ownerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null)
        {
            //if (projectile != null)
            //{
            //    position = projectile.transform.position;
            //    rotation = projectile.transform.forward;
            //}
            //var no = Instantiate(networkSpawner, position,Quaternion.LookRotation(Vector3.forward)).GetComponent<NetworkObject>();
            //no.gameObject.GetComponent<ClientPerkSpawn>().perk = this;
            //no.Spawn();

        }


        public override void TriggerDeathPerk(ProjectileCore projectile = null, ulong ownerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null)
        {
            // First, resolve the reference to a real NetworkObject
            if (hitRef.TryGet(out NetworkObject hitObject))
            {

                // Now try to get the component from the resolved object
                if (!hitObject.TryGetComponent<EnemyPerkHandler>(out var handler))
                    return;

                // Finally call the ClientRpc on the correct client only
                handler.ApplyDeathPerkEffects(this, hitRef);
                Debug.LogError("PERK APPLIED DU LOSER");

            }
        }

        public override void ServerTrigger(Vector3 position, Vector3 rotation,ulong clientID, int id = -1, NetworkObjectReference hitRef = default)
        {
            if(gameObjectToSpawnOnServer != null)
            {
                
                var no = Instantiate(gameObjectToSpawnOnServer, SceneManager.GetSceneByName("World"));
                no.GetComponent<Transform>().position = position;
                no.GetComponent<Transform>().rotation = Quaternion.LookRotation(rotation);
            }
        }

        public override void ClientTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1)
        {
            if (gameObjectToSpawnOnClient != null)
            {
                var no = Instantiate(gameObjectToSpawnOnClient, SceneManager.GetSceneByName("World"));
                no.GetComponent<Transform>().position = position;
                no.GetComponent<Transform>().rotation = Quaternion.LookRotation(rotation);
            }
        }

        public override void PerkUpdate()
        {
            
        }

        public override void PerkApply(Vector3 position, Vector3 rotation, NetworkObjectReference hitRef = default)
        {
         
        }
    }
}