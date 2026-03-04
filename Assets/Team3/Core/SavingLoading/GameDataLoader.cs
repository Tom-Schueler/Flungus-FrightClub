using Team3.SavingLoading.SaveData;
using UnityEngine;

namespace Team3.SavingLoading
{
    public class GameDataLoader : MonoBehaviour
    {
        public void Load()
        {
            GameData.Singleton.TryRead();
        }
    }
}
