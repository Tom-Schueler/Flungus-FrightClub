using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KekwDetlef.SceneManagement
{
    public class SceneRecaller : SceneChanger
    {
        public UnityEvent OnSceneChangeStarted;
        public UnityEvent OnSceneChangeEnded;

        public async void RecallRememeberedSceneCompound()
        {
            if (RuntimeSceneContainer.rememberedUICompound.Count == 0 && RuntimeSceneContainer.rememberedWorldCompound.Count == 0)
            { return; }

            await RetreveKey();

            OnSceneChangeStarted?.Invoke();
            await ChangeAsync();

            ReturnKey();
            OnSceneChangeEnded?.Invoke();
        }

        private async Task ChangeAsync()
        {
            List<Task> loadTasks = new List<Task>();

            HashSet<SceneMap.WorldScene> newWorldSceneSet = LoadWorldScenes(loadTasks);
            HashSet<SceneMap.UIScene> newUISceneSet = LoadUIScenes(loadTasks);

            await Task.WhenAll(loadTasks);

            UnloadUIScenes(newUISceneSet);
            UnloadWorldScenes(newWorldSceneSet);
        }

        private HashSet<SceneMap.UIScene> LoadUIScenes(List<Task> loadTasks)
        {
            HashSet<SceneMap.UIScene> newScenesSet = new HashSet<SceneMap.UIScene>();
            foreach (SceneMap.UIScene scene in RuntimeSceneContainer.rememberedUICompound)
            {
                newScenesSet.Add(scene);

                if (!RuntimeSceneContainer.activeUISceneMap.ContainsKey(scene))
                {
                    AssetReference sceneAsset = SceneMap.uiSceneMap[scene];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true);
                    RuntimeSceneContainer.activeUISceneMap.Add(scene, handle);

                    loadTasks.Add(handle.Task);
                }
            }

            return newScenesSet;
        }

        private HashSet<SceneMap.WorldScene> LoadWorldScenes(List<Task> loadTasks)
        {
            HashSet<SceneMap.WorldScene> newScenesSet = new HashSet<SceneMap.WorldScene>();
            foreach (SceneMap.WorldScene scene in RuntimeSceneContainer.rememberedWorldCompound)
            {
                newScenesSet.Add(scene);

                if (!RuntimeSceneContainer.activeWorldSceneMap.ContainsKey(scene))
                {
                    AssetReference sceneAsset = SceneMap.worldSceneMap[scene];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true);
                    RuntimeSceneContainer.activeWorldSceneMap.Add(scene, handle);

                    loadTasks.Add(handle.Task);
                }
            }

            return newScenesSet;
        }

        private void UnloadWorldScenes(HashSet<SceneMap.WorldScene> newWorldSceneSet)
        {
            List<SceneMap.WorldScene> activeScenes = RuntimeSceneContainer.activeWorldSceneMap.Keys.ToList();

            foreach (SceneMap.WorldScene activeScene in activeScenes)
            {
                if (!newWorldSceneSet.Contains(activeScene))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeWorldSceneMap[activeScene]);
                    RuntimeSceneContainer.activeWorldSceneMap.Remove(activeScene);
                }
            }
        }

        private void UnloadUIScenes(HashSet<SceneMap.UIScene> newUISceneSet)
        {
            List<SceneMap.UIScene> activeScenes = RuntimeSceneContainer.activeUISceneMap.Keys.ToList();

            foreach (SceneMap.UIScene activeScene in activeScenes)
            {
                if (!newUISceneSet.Contains(activeScene))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeUISceneMap[activeScene]);
                    RuntimeSceneContainer.activeUISceneMap.Remove(activeScene);
                }
            }   
        }
    }
}
