using System.Collections.Generic;

using Team3.Enemys.Common.States;
using Team3.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Team3.Enemys
{
    public class PatrolState : State
    {
        [SerializeField] public List<Waypoint> waypoints = new List<Waypoint>();

        [SerializeField] private bool shouldWait = false;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float maxWaitingTime;
        [SerializeField] private Animator anim;

        [SerializeField] private float switchDirectionProbability;

        private int currentWaypointIndex;
        private bool moveForward = true;
        private bool isPatrolling;
        private bool isWaiting = false;
        private float currentWaitingTime;


        


        protected void Awake()
        {
            if (waypoints != null && waypoints.Count > 1)
            {
                currentWaypointIndex = 0;
            }
        }

        public override void Enter()
        {
            SetDestination();
        }

        public override void Update()
        {
            if (isPatrolling && agent.remainingDistance < 1f)
            {
                isPatrolling = false;

                if (shouldWait)
                {
                    isWaiting = true;
                    currentWaitingTime = 0;
                }
                else
                {
                    NextWaypoint();
                    SetDestination();
                }
            }

            if (isWaiting)
            {
                currentWaitingTime += Time.deltaTime;
                if (currentWaitingTime >= maxWaitingTime)
                {
                    isWaiting = false;
                    NextWaypoint();
                    SetDestination();
                }
                //anim.SetFloat("Speed", 0);
            }
            else
            {
                anim.SetBool("Move", true);
            }
        }

        private void NextWaypoint()
        {
            if (moveForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = waypoints.Count - 1;
                }
            }
        }

        private void SetDestination()
        {
            if (waypoints.Count <= 0)
                return;

            Vector3 target = waypoints[currentWaypointIndex].transform.position;
            agent.destination = target;
            isPatrolling = true;
        }


        public override void Exit() { }

        public override void PhysicsUpdate(float delta) { }
    }
}