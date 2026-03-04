using Team3.Enemys.Common.States;
using Team3.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Team3.Enemys.Common
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float chasingRadius;
        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private NavMeshAgent agent;

        [Space]
        [Header("States")]
        [Space]

        [SerializeField] private AIAttackState attackState;
        [SerializeField] private AIChaseState chaseState;
        [SerializeField] private AISearchState searchState;
        [SerializeField] private PatrolState patrolState;
        [SerializeField] private bool isMinion = false;
        [SerializeField] private Rigidbody rb;

        public FiniteStateMachine FSM { get; private set; }

        private void Awake()
        {

        }

        private void Start()
        {
            if (rb != null)
                rb.isKinematic = true;

            NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas);
            transform.position = hit.position;
            agent.enabled = true;

            FSM = new FiniteStateMachine(searchState);
        }

        private void Update()
        {
            FSM.CurrentState.Update();
            EvaluateState();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            FSM.CurrentState.PhysicsUpdate(delta);
        }

        private void EvaluateState()
        {
            
            // check if player is in range for chasing or not and switch state
            Collider[] chasehits = Physics.OverlapSphere(transform.position, chasingRadius, playerLayer);
            bool playerInChaseRange = chasehits.Length > 0;

            Collider[] attackhits = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);
            bool playerInAttackRange = attackhits.Length > 0;

            if (FSM.CurrentState != attackState && playerInAttackRange == true)
            {
                if (attackhits.Length > 0)
                {
                    // TODO: chose player based on distance or something
                    int randomIndex = Random.Range(0, attackhits.Length);
                    GameObject player = attackhits[randomIndex].gameObject;
                    attackState.SetReference(player.transform);
                    FSM.ChangeState(attackState);
                }
            }

            else if (FSM.CurrentState != chaseState && playerInChaseRange == true && playerInAttackRange == false)
            {
                if (chasehits.Length > 0)
                {
                    // TODO: chose player based on distance or something
                    int randomIndex = Random.Range(0, chasehits.Length);
                    GameObject player = chasehits[randomIndex].gameObject;
                    chaseState.SetReference(player.transform);
                    FSM.ChangeState(chaseState);
                }
            }

            else if (FSM.CurrentState != patrolState && isMinion && playerInChaseRange == false && playerInAttackRange == false)
            {  
                FSM.ChangeState(patrolState);
            }

            else if (FSM.CurrentState != searchState && playerInChaseRange == false && playerInAttackRange == false && !isMinion)
            {
                FSM.ChangeState(searchState);
            }

        }

    

        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chasingRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}