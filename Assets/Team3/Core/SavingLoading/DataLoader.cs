using Team3.SavingLoading.SaveData;
using UnityEngine;

namespace Team3.SavingLoading
{
    public class DataLoader : MonoBehaviour
    {
        [SerializeField] private SaveableBehaviour[] loadableObjects;

        public void LoadData()
        {
            if (SettingsData.Singleton.TryRead() == false)
            {
                Debug.LogWarning("Neither Settings nor DefaultSettings could be loaded. Is is fucked.");
                return;
            }
            
            foreach (SaveableBehaviour data in loadableObjects)
            {
                data.Load();
            }
        }
    }
}
