using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using Team3.Weapons;
using Unity.VisualScripting.FullSerializer;

namespace Team3.Combat { }
//{

//    [CreateAssetMenu(fileName = "MagneticShot", menuName = "Team3/Combat/Perk/MagneticShot")]
//    public class MagneticShot : SOProjectilePerk
//    {
//        [SerializeField] private GameObject networkSpawner;
//        [SerializeField] private float correctionForce;
//        [SerializeField] private LayerMask targetLayers;
//        [SerializeField] private float searchRadius;
//        [SerializeField] private float searchDistance;




//        [SerializeField] public int activationDelay;
//        [SerializeField] private GameObject Drone;
//        [SerializeField] private float damage;
//        [SerializeField] private float speed;
//        [SerializeField] private SOVFX explosionFX;
      





//        public override void AdjustStats(ProjectileCore projectile)
//        {

//        }

//        public override void TriggerPerk(ProjectileCore projectile = null, ulong ownerID = 420, Transform transform = null, Collider target = null, Collision collision = null)
//        {
//            Vector3 origin = transform.position;
//            Vector3 direction = transform.forward;
//            Ray ray = new Ray(origin, direction);

//            if (Physics.SphereCast(ray, searchRadius, out RaycastHit hitInfo, searchDistance, targetLayers))
//            {
//                Debug.LogError(hitInfo.collider.gameObject.name);
//                var dir = hitInfo.collider.gameObject.transform.position - transform.position;

//                transform.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * correctionForce, ForceMode.Acceleration);
//            }
//        }

   

//        public override void ServerTrigger(Transform transform, ulong clientID, int id = -1)
//        {
//            // Pass `PerkSpawner` (NetworkBehaviour context) to handle networking
//            if (transform.TryGetComponent<PerkSpawner>(out var spawner))
//            {
//                //ServerFireBall(transform,id);
//            }
//            else
//            {
//                Debug.LogWarning("No PerkSpawner found on transform.");
//            }
//        }

//        public override void ClientTrigger(Transform transform, ulong clientID, int id = -1)
//        {
//            //ClientFireBall(transform,id);
//        }



//        public override void PerkUpdate()
//        {

//        }

//        public override void PerkApply(Transform t)
//        {

//        }

//        [ServerRpc(RequireOwnership = false)]
//        private void TriggerPerkServerRpc(Vector3 position, Quaternion rotation, ulong ownerID)
//        {
//            Transform dummy = new GameObject("PerkSpawnDummy").transform;
//            dummy.position = position;
//            dummy.rotation = rotation;
//            SpawnPerk(dummy, ownerID);
//            Destroy(dummy.gameObject); // clean up
//        }

//        private void SpawnPerk(Transform transform, ulong ownerID)
//        {
//            var obj = Instantiate(networkSpawner, transform.position, transform.rotation).GetComponent<NetworkObject>();
//            var spawner = obj.GetComponent<PerkSpawner>();
//            spawner.perk = this;
//            spawner.OwnerID = ownerID;
//            obj.Spawn();
//        }


//        //public void ClientFireBall(Transform transform, int id = -1)
//        //{
//        //    Debug.LogError(id + " EXPLOSION ID");
//        //    Instantiate(PerkDatabase.Instance.GetVFXByID(id).VFX, transform.position, Quaternion.identity);
//        //}


//        //public void ServerFireBall(Transform transform, int id = -1)
//        //{
//        //    Collider[] hits = Physics.OverlapSphere(transform.position, ExplosionRadius, AffectedLayers);

//        //    foreach (var hit in hits)
//        //    {
//        //        Rigidbody rb = hit.attachedRigidbody;
//        //        if (rb != null)
//        //        {

//        //            if (hit.TryGetComponent<NetworkCharacter>(out var move))
//        //            {
//        //                move.ApplyExplosionForceClientRpc(transform.position, ExplosionForce, hit.GetComponent<NetworkObject>().OwnerClientId);
//        //            }

//        //            else
//        //            {
//        //                rb.AddExplosionForce(ExplosionForce*100, transform.position, ExplosionRadius);
//        //            }

//        //            if (hit.TryGetComponent<CharacterStats>(out var stats))
//        //            {
//        //                stats.TakeDamage(Damage, DamageType.Fire, FireStacks);
//        //                Debug.LogError("SO EIN FIREBALL JUNGE" + Damage);
//        //            }
//        //        }
//        //    }
//        //}



//    }
//}