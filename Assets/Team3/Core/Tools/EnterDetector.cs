using UnityEngine;
using UnityEngine.Events;

namespace Team3.Tools
{
    public class EnterDetector : MonoBehaviour
    {
        [SerializeField] private string tagString;

        public UnityEvent<Collider> OnEnterd;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(tagString))
            { return; }
            
            OnEnterd?.Invoke(other);
        }
    }
}
