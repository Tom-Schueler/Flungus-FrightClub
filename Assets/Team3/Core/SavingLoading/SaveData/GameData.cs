using System;
using System.IO;
using Newtonsoft.Json;
using Team3.Configs;

namespace Team3.SavingLoading.SaveData
{
    [Serializable]
    public class GameData : ISaveData
    {
        // Sigelton Bheaviour
        private static GameData singleton;

        public static GameData Singleton
        {
            get
            {
                singleton ??= new GameData();
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }

        private GameData() { }


        public bool TryRead()
        {
            if (!File.Exists(PathConfigs.Files.GameData))
            { return false; }

            string content = File.ReadAllText(PathConfigs.Files.GameData);
            singleton = JsonConvert.DeserializeObject<GameData>(content);
            return true;
        }

        public void Write()
        {
            string directory = Path.GetDirectoryName(PathConfigs.Files.GameData);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string jsonString = JsonConvert.SerializeObject(singleton, Formatting.Indented);

            File.WriteAllText(PathConfigs.Files.GameData, jsonString);
        }

        public string name;
    }
}
