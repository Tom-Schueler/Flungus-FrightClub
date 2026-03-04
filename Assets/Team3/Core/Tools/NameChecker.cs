using Team3.SavingLoading.SaveData;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;

namespace Team3.Tools
{
    public class NameChecker : MonoBehaviour
    {
        [SerializeField] private bool shouldExecuteOnStart;

        public UnityEvent OnNoNameCached;
        public UnityEvent OnNameCached;

        private void Start()
        {
            if (shouldExecuteOnStart)
            {
                CheckName();
            }
        }

        public void CheckName()
        {
            if (GameData.Singleton.name.IsNullOrEmpty())
            {
                OnNoNameCached?.Invoke();
            }
            else
            {
                OnNameCached?.Invoke();
            }
        }
    }
}
