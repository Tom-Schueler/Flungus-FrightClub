using Team3.Enemys.Common;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Enemys.RangedEnemy
{
    public class BaseRangedAttack : EnemyAttack
    {
        [SerializeField] private Transform spawnPoint;

        public override void UpdateState()
        {
            base.UpdateState();
            if (!hasAttacked)
            {
                hasAttacked = true;
                Attack();
                cooldownTimer = 0;
                agent.speed = 0;
                Invoke(nameof(ResetSpeed), 2);
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

        public override void Attack()
        {
            Debug.LogError("SO NE GEISTERSCHELLE");
            base.Attack();
            if (attackData is BasicRangeAttackData rangeData)
            {
                if (playerTransform == null)
                { return; }

                if (IsOwner)
                {
                    FireGhostProjectileServerRpc(
                        spawnPoint.position,
                        Quaternion.LookRotation(playerTransform.position - spawnPoint.position),
                        AttackData.Damage,
                        AttackData.DamageType);
                }
            }
        }

        public override void ResetAttack()
        {
            base.ResetAttack();
        }

        private void ResetSpeed()
        {
            agent.speed = AttackData.Speed;
        }


        [ServerRpc]
        private void FireGhostProjectileServerRpc(Vector3 spawnPos, Quaternion dir, float dmg, DamageType dtype)
        {
            if (AttackData is BasicRangeAttackData rangeData)
            {
                GameObject projGO = Instantiate(rangeData.Projectile, spawnPos, dir);
                var ghostProj = projGO.GetComponent<GhostProjectile>();
                var netObj = projGO.GetComponent<NetworkObject>();

                ghostProj.Initialise(projGO.transform.forward, dmg, (DamageType)dtype, rangeData.Speed);
                netObj.Spawn();
            }
        }
    }
}
