using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Team3.Characters;
using Team3.Enemys.Common;

namespace Team3.Weapons
{
    public class BulletObject : NetworkBehaviour
    {
        public List<SOBulletPerk> perkList;
        public List<DamageTypeValue> damageTypeDamage;
        public DamageType type;
        public DamageType affix;
        public float damage;
        public float damagePerBullet;

        public float force;
        public float gravityMultiplier;
        public float lifeTime;
        public bool isDestroyedOnImpact;
        public int numberOfAncestors;
        public GameObject trailFX;
        public GameObject impactFX;
        public GameObject spawnFX;
        public GameObject projectileMesh;
        public float collisionRadius;
        public GameObject ownerObject;

        [SerializeField]
        public HashSet<GameObject> hitEnemies = new HashSet<GameObject>();
        private Rigidbody rigidBody;

        

        public bool isDestroyed = false;

       
        private GameObject bulletTrail;

        private void OnEnable()
        {
            rigidBody = gameObject.GetComponent<Rigidbody>();
        }

        public void OnSpawn()
        {
            GameObject bulletMesh = Instantiate(projectileMesh, gameObject.transform);
            //GameObject bulletSpawnFX = Instantiate(spawnFX, transform.position, Quaternion.identity);
            bulletTrail = Instantiate(trailFX, transform.position, Quaternion.identity);
            bulletTrail.GetComponent<TrailSmooth>().target = transform;
            bulletTrail.GetComponent<TrailSmooth>().lifeTime = lifeTime;
            GetComponent<SphereCollider>().radius = collisionRadius;
            AddBulletVelocity();



            if (perkList != null)
            {
                foreach (SOBulletPerk perk in perkList)
                {
                    perk.OnSpawn(this);
                }
            }
        }

        public void AddBulletVelocity()
        {          
            rigidBody.AddForce(transform.forward * force * 100);
        }

        private void FixedUpdate()
        {

            Vector3 customGravity = gravityMultiplier * Physics.gravity;
            rigidBody.AddForce(customGravity, ForceMode.Acceleration);

        }

        private void Update()
        {
           
            OnUpdate();
        }



        private void OnUpdate()
        {
            Vector3 velocity = rigidBody.linearVelocity;
            if (velocity.sqrMagnitude > 0.01f)
            {
                // Rotate the forward direction to match the velocity
                transform.rotation = Quaternion.LookRotation(velocity.normalized);
            }

            if (perkList != null)
            {
                foreach (SOBulletPerk perk in perkList)
                {
                    perk.OnUpdate(this);
                }
            }


            lifeTime -= Time.deltaTime;

            if(lifeTime < 0 && !isDestroyed)
            {
                isDestroyed = true;
                OnDestruction();
                


            }

        }

        private void OnDestruction()
        {
            isDestroyed = true;
            Instantiate(impactFX,transform.position, Quaternion.identity);
            if (perkList != null)
            {
                foreach (SOBulletPerk perk in perkList)
                {
                    perk.OnDestruction(this);
                    
                }
            }

            
            if (IsServer && NetworkObject.IsSpawned)
            {
                NetworkObject.Despawn();
                
            }
            else
            {
                Destroy(gameObject);
                
            }
        }

        private void OnImpact(CharacterStats impactObject)
        { 
            if(damagePerBullet < damage) { 
            impactObject.gameObject.GetComponent<EnemyHit>().TakeDamage(damagePerBullet, type, affix,1);
                damagePerBullet = damage;
            }
            else
            {
                impactObject.gameObject.GetComponent<EnemyHit>().TakeDamage(damage, type, affix,1);
            }
                


            if (numberOfAncestors >= 0)
            {
                Instantiate(impactFX, transform.position, Quaternion.identity);
            


            if (perkList != null)
            {
                foreach (SOBulletPerk perk in perkList)
                {
                    perk.OnImpact(this, impactObject);
                }
            }
            }

           
            if (isDestroyedOnImpact)
            {
                OnDestruction();
            }
        }




        void OnTriggerEnter(Collider other)
        {
            if (isDestroyed) return;
            if (other.gameObject == ownerObject) return; 


            if ((other.CompareTag("Enemy") || other.CompareTag("Player")) && !hitEnemies.Contains(other.gameObject))
            {
                CharacterStats targetStats = other.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    
                    hitEnemies.Add(other.gameObject);
                    OnImpact(targetStats);
                }
            }

            else if (isDestroyedOnImpact && !(other.CompareTag("Enemy") || other.CompareTag("Player")))
            {
                OnDestruction();
            }
        }
    }
}