using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KekwDetlef.SceneManagement
{
    public class MultipleSceneChanger : SceneChanger
    {
        [SerializeField] private UISceneInfo[] uiSceneInfo;
        [SerializeField] private WorldSceneInfo[] worldSceneInfo;

        public UnityEvent OnSceneChangeStarted;
        public UnityEvent OnSceneChangeEnded;

        public async void ChangeScene()
        {
            await RetreveKey();

            OnSceneChangeStarted?.Invoke();
            await StartChangeAsync();

            ReturnKey();

            OnSceneChangeEnded?.Invoke();
        }

        private async Task StartChangeAsync()
        {
            List<Task> loadTasks = new List<Task>();

            HashSet<SceneMap.WorldScene> newWorldSceneSet = LoadWorldScenes(ref loadTasks);
            HashSet<SceneMap.UIScene> newUISceneSet = LoadUIScenes(ref loadTasks);

            await Task.WhenAll(loadTasks);

            UnloadUIScenes(newUISceneSet);
            UnloadWorldScenes(newWorldSceneSet);
        }

        private HashSet<SceneMap.UIScene> LoadUIScenes(ref List<Task> loadTasks)
        {
            HashSet<SceneMap.UIScene> newScenesSet = new HashSet<SceneMap.UIScene>();
            foreach (var info in uiSceneInfo)
            {
                newScenesSet.Add(info.type);

                if (!RuntimeSceneContainer.activeUISceneMap.ContainsKey(info.type))
                {
                    AssetReference sceneAsset = SceneMap.uiSceneMap[info.type];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true, info.priority);
                    RuntimeSceneContainer.activeUISceneMap.Add(info.type, handle);

                    loadTasks.Add(handle.Task);
                }
                else if (info.shouldReload)
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeUISceneMap[info.type]);
                    RuntimeSceneContainer.activeUISceneMap.Remove(info.type);

                    AssetReference sceneAsset = SceneMap.uiSceneMap[info.type];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true, info.priority);
                    RuntimeSceneContainer.activeUISceneMap.Add(info.type, handle);

                    loadTasks.Add(handle.Task);
                }
            }

            return newScenesSet;
        }

        private HashSet<SceneMap.WorldScene> LoadWorldScenes(ref List<Task> loadTasks)
        {
            HashSet<SceneMap.WorldScene> newScenesSet = new HashSet<SceneMap.WorldScene>();
            foreach (var info in worldSceneInfo)
            {
                newScenesSet.Add(info.type);

                if (!RuntimeSceneContainer.activeWorldSceneMap.ContainsKey(info.type))
                {
                    AssetReference sceneAsset = SceneMap.worldSceneMap[info.type];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true, info.priority);
                    RuntimeSceneContainer.activeWorldSceneMap.Add(info.type, handle);

                    loadTasks.Add(handle.Task);
                }
                else if (info.shouldReload)
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeWorldSceneMap[info.type]);
                    RuntimeSceneContainer.activeWorldSceneMap.Remove(info.type);

                    AssetReference sceneAsset = SceneMap.worldSceneMap[info.type];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true, info.priority);
                    RuntimeSceneContainer.activeWorldSceneMap.Add(info.type, handle);

                    loadTasks.Add(handle.Task);
                }
            }

            return newScenesSet;
        }

        private void UnloadWorldScenes(HashSet<SceneMap.WorldScene> newScenesSet)
        {
            List<SceneMap.WorldScene> activeScenes = RuntimeSceneContainer.activeWorldSceneMap.Keys.ToList();

            foreach (SceneMap.WorldScene activeScene in activeScenes)
            {
                if (!newScenesSet.Contains(activeScene))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeWorldSceneMap[activeScene]);
                    RuntimeSceneContainer.activeWorldSceneMap.Remove(activeScene);
                }
            }
        }

        private void UnloadUIScenes(HashSet<SceneMap.UIScene> newScenesSet)
        {
            List<SceneMap.UIScene> activeScenes = RuntimeSceneContainer.activeUISceneMap.Keys.ToList();

            foreach (SceneMap.UIScene activeScene in activeScenes)
            {
                if (!newScenesSet.Contains(activeScene))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeUISceneMap[activeScene]);
                    RuntimeSceneContainer.activeUISceneMap.Remove(activeScene);
                }
            }
        }
    }
}
