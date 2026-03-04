using UnityEngine;
using UnityEngine.AI;

namespace Team3.Enemys
{
    public class BaseState : MonoBehaviour
    {
        [SerializeField]
        protected NavMeshAgent agent;

        [SerializeField]
        protected Animator animator;

        [SerializeField]
        protected float speed;

        protected virtual void Awake()
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }
        }
    }
}