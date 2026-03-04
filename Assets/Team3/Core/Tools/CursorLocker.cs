using UnityEngine;

namespace Team3.Tools
{
    public class CursorLocker : MonoBehaviour
    {
        [SerializeField] private bool setOnStart = false;
        
        private void Start()
        {
            if (setOnStart)
            {
                Lock();
            }
        }
        
        public void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
