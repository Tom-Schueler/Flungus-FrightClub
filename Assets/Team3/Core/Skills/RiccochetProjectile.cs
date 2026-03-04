using System.Collections.Generic;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;
using Unity.VisualScripting;
using Team3.Enemys.Common;

namespace Team3.Skills
{
    [RequireComponent(typeof(Rigidbody))]

    public class RiccochetProjectile : NetworkBehaviour
    {
        [SerializeField] private GameObject hitFX;
        [SerializeField] private List<GameObject> hitTarget = new List<GameObject>();

        [Header("Stats")]
        public float launchForce = 200f;
        public float homingDelay = 0.3f;
        public float homingStrength = 100f;
        public float lifetime = 5f;
        public float maxVelocity = 50f;

        [Header("Combat")]
        public float damage;
        public DamageType type;
        public DamageType affix;
        public GameObject user;

        private Rigidbody rb;
        private Transform target;
        private float timeAlive;
        private bool isHoming;
        public HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

        public void AddTarget(GameObject t)
        {
            if (!hitTarget.Contains(t))
            {
                hitTarget.Add(t);
            }
        }

        public override void OnNetworkSpawn()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
        }

        public void Initialize(Vector3 initialDirection, Transform targetTransform)
        {
            if (IsServer)
            {
                if (rb == null)
                {
                    rb = GetComponent<Rigidbody>();
                }

                target = targetTransform;

                rb.linearVelocity = Vector3.zero;
                rb.AddForce(initialDirection.normalized * launchForce, ForceMode.Impulse);
            }
        }

        private void FixedUpdate()
        {
            Debug.Log("ICH Sterbe");
            if (!IsServer)
            { return; }

            timeAlive += Time.fixedDeltaTime;

            if (timeAlive > lifetime)
            {
                NetworkObject.Despawn();
                return;
            }

            if (!isHoming && timeAlive >= homingDelay)
            {
                isHoming = true;
            }

            if (isHoming && target != null)
            {
                Vector3 toTarget = (target.position - transform.position).normalized;
                rb.AddForce(toTarget * homingStrength * Time.fixedDeltaTime, ForceMode.Acceleration);

                if (rb.linearVelocity.magnitude > maxVelocity)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
                }
            }
            Debug.Log(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;

            if (other.gameObject == user) return; // don't hit caster
            if (hitEnemies.Contains(other.gameObject)) return;

            if (target != null && other.gameObject != target.gameObject) return;

            if (other.CompareTag("Enemy"))
            {
                hitEnemies.Add(other.gameObject);

                // Apply damage
                if (!other.gameObject.IsDestroyed() && other.TryGetComponent(out EnemyHit enemyHit))
                {
                    enemyHit.TakeDamage(damage, type, affix, 1);
                }

                FindNewTarget(other.transform.position);
            }
        }

        private void FindNewTarget(Vector3 hitPoint)
        {
            float chainRadius = 50f;
            LayerMask enemyLayer = LayerMask.GetMask("Enemy");

            Collider[] hits = Physics.OverlapSphere(hitPoint, chainRadius, enemyLayer);
            Debug.Log($"OverlapSphere hit {hits.Length} colliders.");

            foreach (Collider col in hits)
            {
                if (hitEnemies.Contains(col.gameObject)) continue;
                if (!col.CompareTag("Enemy")) continue;

                Debug.Log("Found new bounce target: " + col.name);
                AttackNext(col.gameObject);
                break;
            }
        }

        private void AttackNext(GameObject newTarget)
        {
            target = newTarget.transform;

            Vector3 bounceDir = -rb.linearVelocity.normalized;
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(bounceDir * launchForce, ForceMode.Impulse);

            timeAlive = 0f;
            isHoming = false;
        }
    }
}
