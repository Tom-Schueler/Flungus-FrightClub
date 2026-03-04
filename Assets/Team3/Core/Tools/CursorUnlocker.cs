using UnityEngine;

namespace Team3.Tools
{
    public class CursorUnlocker : MonoBehaviour
    {
        [SerializeField] private bool setOnStart = false;

        private void Start()
        {
            if (setOnStart)
            {
                Unlock();    
            }
        }

        public void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
