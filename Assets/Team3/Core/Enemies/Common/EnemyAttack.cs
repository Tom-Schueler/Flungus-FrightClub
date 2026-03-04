using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

namespace Team3.Enemys.Common
{
    public class EnemyAttack : NetworkBehaviour
    {
        [SerializeField] protected SOAttackData attackData;
        [SerializeField] protected NavMeshAgent agent;

        public SOAttackData AttackData => attackData;

        protected float cooldownTimer = 0;
        protected bool hasAttacked = false;
        protected Transform playerTransform;

        public virtual void Enter(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        public virtual void UpdateState()
        {
            if (playerTransform != null)
            {
                Vector3 direction = transform.position - transform.position;
                direction.y = 0f;

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * AttackData.RotationSpeed);
                }
            }
        }

        public virtual void Attack() { }
        public virtual void ResetAttack() { }
        public virtual void Exit() { }
    }
}
