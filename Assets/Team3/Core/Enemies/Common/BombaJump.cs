using UnityEngine;
using Team3.Enemys.Common;
using Unity.Netcode;
using Team3.Characters;
using System.Collections;
using UnityEngine.AI;


namespace Team3.Enemys
{
    public class BombaJump : EnemyAttack
    {
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject explosionVFX;
        [SerializeField] private Transform impactPoint;

        [SerializeField] private float ExplosionRadius;
        [SerializeField] private float ExplosionForce;
        [SerializeField] private LayerMask AffectedLayers;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float jumpForce = 20;
        bool canExplode = false;



        [ClientRpc]
        public void ExplosionClientRpc(Vector3 position)
        {
            Instantiate(explosionVFX, position, Quaternion.identity);
        }

        public override void Attack()
        {
            Debug.LogError("ATTACK JUMP BOOM");
            anim.SetTrigger("Attack");
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (!hasAttacked)
            {
                hasAttacked = true;
                cooldownTimer = 0;
                Attack();
            }
            else
            {
                cooldownTimer += Time.deltaTime;
            }

            if (cooldownTimer > attackData.Cooldown)
            {
                hasAttacked = false;
                ResetAttack();
            }
        }

        public void Leap()
        {
            StartCoroutine(Leaping());
            canExplode = true;
        }
        public IEnumerator Leaping()
        {
            yield return null;
            agent.enabled = false;
            rb.useGravity = true;
            rb.isKinematic = false;
            if(playerTransform = null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce((playerTransform.position - transform.position + Vector3.up).normalized * jumpForce, ForceMode.Impulse);
            }

                yield return new WaitForFixedUpdate();
            //yield return new WaitUntil(() => rb.linearVelocity.magnitude < 0.05f);
            yield return new WaitForSeconds(5f);
            canExplode = false;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 30f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
            else
            {
                gameObject.GetComponent<EnemyStats>().TakeDamage(200);
            }

                agent.enabled = true;

            yield return null;

        }

        private void ResetSpeed()
        {
            agent.speed = AttackData.Speed;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (canExplode)
            {
                if (other.CompareTag("Player"))
                {
                    explode();
                }
            }
        }
        public void explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, ExplosionRadius, AffectedLayers);

            if (hits.Length > 0)
            {
                ExplosionClientRpc(transform.position);
                foreach (var hit in hits)
                {
                    Rigidbody rb = hit.attachedRigidbody;
                    if (rb != null)
                    {

                        if (hit.TryGetComponent<NetworkCharacter>(out var move))
                        {
                            move.ApplyExplosionForceClientRpc(transform.position - (Vector3.up * 3), ExplosionForce, hit.GetComponent<NetworkObject>().OwnerClientId);
                        }

                    }
                }
            }

            gameObject.GetComponent<NetworkObject>().Despawn(true);
        }
        public void SpawnImpact()
        {

        }
    }
}