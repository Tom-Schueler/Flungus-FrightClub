using Team3.SavingLoading.SaveData;
using UnityEngine;

namespace Team3.SavingLoading
{
    public class GameDataSaver : MonoBehaviour
    {
        public void Save()
        {
            GameData.Singleton.Write();
        }
    }
}
