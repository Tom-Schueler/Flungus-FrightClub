using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace KekwDetlef.SceneManagement
{
    public static class RuntimeSceneContainer
    {
        public static Action OnKeyReturned;
        private static bool keyOccupied = false;

        public static Dictionary<SceneMap.UIScene, AsyncOperationHandle<SceneInstance>> activeUISceneMap
            = new Dictionary<SceneMap.UIScene, AsyncOperationHandle<SceneInstance>>();
        public static Dictionary<SceneMap.WorldScene, AsyncOperationHandle<SceneInstance>> activeWorldSceneMap
            = new Dictionary<SceneMap.WorldScene, AsyncOperationHandle<SceneInstance>>();


        public static List<SceneMap.UIScene> rememberedUICompound = new List<SceneMap.UIScene>();
        public static List<SceneMap.WorldScene> rememberedWorldCompound = new List<SceneMap.WorldScene>();
        
        
        public static bool TryRetreveKey()
        {
            if (keyOccupied)
            { return false; }

            keyOccupied = true;
            return true;
        }

        public static void ReturnKey()
        {
            if (!keyOccupied)
            { return; }

            keyOccupied = false;
            OnKeyReturned?.Invoke();
        }
    }
}
