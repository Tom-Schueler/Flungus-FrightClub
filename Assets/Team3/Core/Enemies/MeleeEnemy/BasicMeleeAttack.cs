using Team3.Enemys.Common;
using UnityEngine;

namespace Team3.Enemys.MeeleEnemy
{
    public class BasicMeleeAttack : EnemyAttack
    {
        [SerializeField] private Animation ani;
        [SerializeField] private ParticleSystem particle;

        public override void Attack()
        {
            particle.Play();
            ani.Play();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (!hasAttacked)
            {
                hasAttacked = true;
                cooldownTimer = 0;
                agent.speed = 0;
                if (AttackData is BasicMeleeAttackData data)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * data.JumpForce, ForceMode.Impulse);
                }

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
    }
}
