using System;
using UnityEngine;
using UnityEngine.Events;

namespace Team3.Tools
{
    public abstract class SingleBehaviour<T> : MonoBehaviour where T : SingleBehaviour<T>
    {
        public UnityEvent OnInstantiated;
        private static T instance;

        /// <summary>
        /// Dont Hide this. It is essential for this to work. If you need to override call the base at the start.
        /// </summary>
        protected void Awake()
        {
            if (this.GetType() != typeof(T))
            {
                throw new InvalidOperationException($"GenericClass<T>: T must be the same as the derived class. Expected {this.GetType().Name} : GenericClass<{this.GetType().Name}>");
            }

            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this as T;
            DontDestroyOnLoad(gameObject);
            OnInstantiated?.Invoke();

            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}


