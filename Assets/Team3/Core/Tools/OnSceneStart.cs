using UnityEngine;
using UnityEngine.Events;

namespace Team3.Tools
{
    public class EmitOnSceneStart : MonoBehaviour
    {
        public UnityEvent OnSceneStart;

        void Start()
        {
            OnSceneStart?.Invoke();
        }
    }
}
