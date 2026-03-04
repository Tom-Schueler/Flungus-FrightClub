using Team3.Characters;
using Team3.Enemys.Common;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Combat
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private SphereCollider sCollider;
        [SerializeField] private float launchForce;
        [SerializeField] public float activationDelay;
        [SerializeField] private float damage;
        [SerializeField] private float speed;
        [SerializeField] private SOVFX explosionFX;
        [SerializeField] private LayerMask targetLayers;
        [SerializeField] private float searchRadius;
        [SerializeField] private int ownerID;

        private Rigidbody rb;
        private bool isActive = false;


        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            LaunchDrone(launchForce);
        }

        public void LaunchDrone(float force)
        {
            rb.AddForce(new Vector3(0f, 1f, 0f) * force, ForceMode.Impulse);
        }

        public void Update()
        {
            activationDelay -= Time.deltaTime;
            if (activationDelay <= 0 && !isActive)
            {
                isActive = true;
                ActivateDrone();
            }
        }


        public void ActivateDrone()
        {
            sCollider = GetComponent<SphereCollider>();
            sCollider.enabled = true;
            var dir = new Vector3(0, -1, 0);

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, targetLayers);
            if (hitColliders.Length > 0)
            {
                dir = (hitColliders[0].gameObject.transform.position + new Vector3(0f,0.5f,0f)) - transform.position;
            }

            //rb = GetComponent<Rigidbody>();
            rb.AddForce(dir.normalized * speed, ForceMode.Impulse);

        }


        public void OnTriggerEnter(Collider other)
        {
            //rb = GetComponent<Rigidbody>();
            //rb.linearVelocity = Vector3.zero;
            //rb.useGravity = false;

            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                if (other.CompareTag("Player")) { 
                if (other.gameObject.TryGetComponent<PlayerStats>(out PlayerStats stats))
                {
                    stats.TakeDamage(damage);
                }
                }
                else
                {
                    if (other.gameObject.TryGetComponent<EnemyStats>(out EnemyStats stats))
                    {
                        stats.TakeDamage(damage,Weapons.DamageType.Ice,1,1,ownerID);
                    }
                }
            }

            Instantiate(explosionFX.VFX, transform.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
    }
}