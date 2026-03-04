using System;
using UnityEngine;

namespace Team3.Configs
{
    public static class PathConfigs
    {
        private static string PersistantPath => Application.persistentDataPath;

        public static class Files
        {
            public static string Settings => $"{PersistantPath}/SavedData/Settings.json";
            // public static string DefaultSettings => throw new NotImplementedException($"There is not path for {DefaultSettings} yet");
            public static string GameData => $"{PersistantPath}/SavedData/GameData.json";
        }

        public static class Folder
        {
            // public static string CharacterSaves => $"{PersistantPath}/SavedData/Characters/";
        }
    }
}
