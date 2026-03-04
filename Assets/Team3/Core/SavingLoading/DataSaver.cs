using Team3.SavingLoading.SaveData;
using UnityEngine;

namespace Team3.SavingLoading
{
    public class DataSaver : MonoBehaviour
    {
        [SerializeField] private SaveableBehaviour[] saveableObects;

        private void OnEnable()
        {
            SavePrompter.OnPromptSave += SaveData;
        }

        private void OnDisable()
        {
            SavePrompter.OnPromptSave -= SaveData;
        }

        public void SaveData()
        {
            if (SettingsData.Singleton.TryRead() == false)
            {
                Debug.LogWarning("Neither Settings nor DefaultSettings could be loaded. Is is fucked.");
            }

            foreach (SaveableBehaviour settingToSave in saveableObects)
            {
                settingToSave.Save();
            }

            SettingsData.Singleton.Write();
        }
    }
}
