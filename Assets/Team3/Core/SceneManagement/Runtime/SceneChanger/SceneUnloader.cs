using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace KekwDetlef.SceneManagement
{
    public class SceneUnloader : SceneChanger
    {        
        [SerializeField] private SceneMap.UIScene[] uiScene;
        [SerializeField] private SceneMap.WorldScene[] worldScene;

        public UnityEvent OnSceneUnloadStarted;
        public UnityEvent OnSceneUnloadEnded;

        public async void UnloadScenes()
        {
            await RetreveKey();

            List<Task> loadTasks = new List<Task>();
            OnSceneUnloadStarted?.Invoke();

            UnloadUIScenes(ref loadTasks);
            UnloadWorldScenes(ref loadTasks);

            await Task.WhenAll(loadTasks);

            OnSceneUnloadEnded?.Invoke();

            ReturnKey();
        }

        private void UnloadWorldScenes(ref List<Task> loadTasks)
        {
            foreach (var sceneInfo in worldScene)
            {
                if (RuntimeSceneContainer.activeWorldSceneMap.ContainsKey(sceneInfo))
                {
                    var handle = RuntimeSceneContainer.activeWorldSceneMap[sceneInfo];
                    Addressables.UnloadSceneAsync(handle);
                    RuntimeSceneContainer.activeWorldSceneMap.Remove(sceneInfo);

                    loadTasks.Add(handle.Task);
                }
            }
        }

        private void UnloadUIScenes(ref List<Task> loadTasks)
        {
            foreach (var sceneInfo in uiScene)
            {
                if (RuntimeSceneContainer.activeUISceneMap.ContainsKey(sceneInfo))
                {
                    var handle = RuntimeSceneContainer.activeUISceneMap[sceneInfo];
                    Addressables.UnloadSceneAsync(handle);
                    RuntimeSceneContainer.activeUISceneMap.Remove(sceneInfo);

                    loadTasks.Add(handle.Task);
                }
            }
        }
    }
}
