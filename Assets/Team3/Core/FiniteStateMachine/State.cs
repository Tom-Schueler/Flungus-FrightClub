using UnityEngine;

namespace Team3.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void PhysicsUpdate(float delta);
    }
}
