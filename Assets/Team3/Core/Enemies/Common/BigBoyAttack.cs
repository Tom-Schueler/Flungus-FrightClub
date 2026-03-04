using UnityEngine;
using Team3.Enemys.Common;
using Team3.Enemys.MeeleEnemy;
using Unity.Netcode;
namespace Team3.Enemys
{
    public class BigBoyAttack : EnemyAttack
    {

        [SerializeField] private Animator anim;
        [SerializeField] private GameObject impact;
        [SerializeField] private Transform impactPoint;

        [ClientRpc]
        public void SpawnImpactClientRpc(Vector3 position)
        {
            Instantiate(impact, impactPoint.position, Quaternion.identity);
        }


        public override void Attack()
        {

            anim.SetTrigger("Attack");


        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (!hasAttacked)
            {
                hasAttacked = true;
                cooldownTimer = 0;
                agent.speed = 0;
                Attack();
                Invoke(nameof(ResetSpeed), 0.2f);
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


        private void ResetSpeed()
        {
            agent.speed = AttackData.Speed;
        }


        public void SpawnImpact()
        {
            SpawnImpactClientRpc(transform.position);
        }
    }
}