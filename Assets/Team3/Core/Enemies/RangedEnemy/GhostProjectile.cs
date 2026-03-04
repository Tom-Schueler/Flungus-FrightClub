using Team3.Characters;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Enemys.RangedEnemy
{
    /// <summary>
    /// Network-aware enemy projectile.
    /// Server-authoritative: it is spawned, moves, collides and despawns only on the server.
    /// Clients receive visual-only updates through ClientRpcs.
    /// </summary>
    [RequireComponent(typeof(NetworkObject), typeof(Rigidbody), typeof(Collider))]
    public class GhostProjectile : NetworkBehaviour
    {
        [Header("Runtime")]
        [SerializeField] private Rigidbody rb;          // filled in Awake if missing
        [SerializeField] private Collider triggerCol;   // filled in Awake if missing

        [Header("Tuning")]
        [SerializeField] private float force = 20f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private DamageType damageType = DamageType.Ice;
        [SerializeField] private float lifeTime = 5f;

        [Header("FX (prefabs registered in the NetworkManager)")]
        [SerializeField] private GameObject hitFX;      // simple VFX (not networked)

        /* ---------- INITIALISATION ---------- */

        private void Awake()
        {
            if (!rb) rb = GetComponent<Rigidbody>();
            if (!triggerCol) triggerCol = GetComponent<Collider>();
            triggerCol.isTrigger = true;
        }

        /// <summary> Called by server immediately after Spawn() </summary>
        public void Initialise(Vector3 direction, float dmg, DamageType dtype, float speed = -1f)
        {
            damage = dmg;
            damageType = dtype;

            if (speed < 0f) speed = force;

            rb.linearVelocity = Vector3.zero;
            rb.AddForce(direction.normalized * speed, ForceMode.Impulse);

            // self-despawn failsafe
            Invoke(nameof(SelfDespawn), lifeTime);
        }

        /* ---------- SERVER-SIDE COLLISION ---------- */

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;                               // only the server processes hits

            if (other.CompareTag("Player") &&
                other.TryGetComponent(out PlayerStats stats))
            {
                stats.TakeDamage(damage, damageType);
                PlayHitFxClientRpc(transform.position, transform.rotation);
                SelfDespawn();
            }
        }

        /* ---------- NETWORK UTILS ---------- */

        [ClientRpc]
        private void PlayHitFxClientRpc(Vector3 pos, Quaternion rot)
        {
            if (hitFX) Instantiate(hitFX, pos, rot);
        }

        private void SelfDespawn()
        {
            if (IsServer && NetworkObject && NetworkObject.IsSpawned)
                NetworkObject.Despawn();
        }
    }
}
