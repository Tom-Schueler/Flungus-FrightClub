using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using Team3.Weapons;
using System.Collections;


namespace Team3.Combat {

    [CreateAssetMenu(fileName = "SpawnBlackHole", menuName = "Team3/Combat/Perk/SpawnBlackHole")]
    public class SpawnBlackHole : SOProjectilePerk
    {
        [SerializeField]
        private GameObject networkSpawner;
        [SerializeField]
        private float explosionRadius;
        [SerializeField]
        private float explosionForce;
       
        [SerializeField]
        private LayerMask affectedLayers;

        [SerializeField]
        private float damage;

        public float ExplosionRadius => explosionRadius;
        public float ExplosionForce => explosionForce;
       
        public LayerMask AffectedLayers => affectedLayers;
        public float Damage => damage;

        public GameObject Cosmetic;

        [SerializeField]
        public int FireStacks = 40;
       
        

        public override void AdjustStats(ProjectileCore projectile)
        {
            
        }

        public override void TriggerPerk(ProjectileCore projectile = null, ulong ownerID = 420, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), NetworkObjectReference hitRef = default, Collision collision = null)
        {
            if (projectile != null)
            {
                position = projectile.transform.position;
                rotation = projectile.transform.forward;
            }
            var no = Instantiate(networkSpawner,position, Quaternion.LookRotation(rotation)).GetComponent<NetworkObject>();
            
            var imp = no.gameObject.GetComponent<Implosion>();
            imp.AffectedLayers = AffectedLayers;
            imp.Damage = damage;
            imp.ExplosionForce = explosionForce;
            imp.ExplosionRadius = explosionRadius;
            
            no.Spawn();
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
                

            }
        }



        public void ClientFireBall(Vector3 position, Vector3 rotation, int id = -1)
        {
            Instantiate(PerkDatabase.Instance.GetVFXByID(20).VFX, position, Quaternion.identity);
        }

        

        public void ServerFireBall(Vector3 position, Vector3 rotation, int id = -1)
        {
            Debug.LogError("SPAWNED A HOLE");
            var no = Instantiate(networkSpawner, position, Quaternion.LookRotation(rotation)).GetComponent<NetworkObject>();

            var imp = no.gameObject.GetComponent<Implosion>();
            imp.AffectedLayers = AffectedLayers;
            imp.Damage = damage;
            imp.ExplosionForce = explosionForce;
            imp.ExplosionRadius = explosionRadius;

            
            no.Spawn();

            //Collider[] hits = Physics.OverlapSphere(position, ExplosionRadius, AffectedLayers);


            //foreach (var hit in hits)
            //{
            //    Rigidbody rb = hit.attachedRigidbody;
            //    if (rb != null)
            //    {

            //        if (hit.TryGetComponent<NetworkCharacter>(out var move))
            //        {
            //            move.ApplyImplosionForceClientRpc(position, ExplosionForce, hit.GetComponent<NetworkObject>().OwnerClientId);
            //        }

            //        else
            //        {
            //            rb.AddExplosionForce(ExplosionForce * 100, position, ExplosionRadius);
            //        }

            //        if (hit.TryGetComponent<CharacterStats>(out var stats))
            //        {
                      
            //        }
            //    }
            //}
        }




        public override void ServerTrigger(Vector3 position , Vector3 rotation, ulong clientID, int id = -1, NetworkObjectReference hitRef = default)
        {
            ServerFireBall( position,  rotation , id);
        }

        public override void ClientTrigger(Vector3 position, Vector3 rotation, ulong clientID, int id = -1)
        {
            ClientFireBall(position,rotation,id);
            //ServerFireBall(position, rotation, id); 
        }



        public override void PerkUpdate()
        {
            
        }

        public override void PerkApply(Vector3 position, Vector3 rotation, NetworkObjectReference hitRef = default)
        {
            var cos = Instantiate(Cosmetic, position,Quaternion.identity);
            if (hitRef.TryGet(out NetworkObject hitObject))
                cos.transform.parent = hitObject.transform;


        }

        [ServerRpc(RequireOwnership = false)]
        private void TriggerPerkServerRpc(Vector3 position, Quaternion rotation, ulong ownerID)
        {
            Transform dummy = new GameObject("PerkSpawnDummy").transform;
            dummy.position = position;
            dummy.rotation = rotation;
            SpawnPerk(dummy, ownerID);
            Destroy(dummy.gameObject); // clean up
        }

        private void SpawnPerk(Transform transform, ulong ownerID)
        {
            var obj = Instantiate(networkSpawner, transform.position, transform.rotation).GetComponent<NetworkObject>();
            var spawner = obj.GetComponent<PerkSpawner>();
            spawner.perk = this;
            spawner.OwnerID = ownerID;
            obj.Spawn();
        }



        
    }
}