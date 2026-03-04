using System;
using UnityEngine;

namespace Team3.UserInterface
{
    public class InventroyStateEmitter : MonoBehaviour
    {
        public static Action OnEntered;
        public static Action OnExited;

        private void Start()
        {
            OnEntered?.Invoke();
        }

        public void Exit()
        {
            OnExited?.Invoke();
        }
    }
} 
