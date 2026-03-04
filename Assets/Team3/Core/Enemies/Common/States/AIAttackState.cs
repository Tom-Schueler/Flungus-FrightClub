using Team3.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Team3.Enemys.Common.States
{
    public class AIAttackState : State
    {
        [SerializeField] private EnemyAttack attackPattern;
        [SerializeField] private NavMeshAgent agent;

        private Transform playerTransform;

        public void SetReference(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        public override void Enter()
        {
            agent.speed = attackPattern.AttackData.Speed;
            agent.stoppingDistance = attackPattern.AttackData.StopDistance;
            attackPattern.Enter(playerTransform);
        }

        public override void Exit()
        {
            attackPattern.Exit();
        }

        public override void Update()
        {
            attackPattern.UpdateState();
        }

        public override void PhysicsUpdate(float delta) { }
    }
}