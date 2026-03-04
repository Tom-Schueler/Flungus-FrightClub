using Team3.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Team3.Enemys.Common.States
{
    public class AIChaseState : State 
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float speed = 0;
        
        private Transform playerTransform;

        public void SetReference(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        public override void Enter()
        {
            agent.speed = speed;
        }

        public override void Exit() { }

        public override void PhysicsUpdate(float delta) { }

        public override void Update()
        {
            if (playerTransform == null)
            { return; }

            if (!agent.isActiveAndEnabled)
                return;

            agent.destination = playerTransform.transform.position;
        }
    }
}