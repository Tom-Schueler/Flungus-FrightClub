using UnityEngine;

namespace Team3.SavingLoading
{
    public abstract class SaveableBehaviour : MonoBehaviour
    {
        public abstract void Save();
        public abstract void Load();
    }
}
