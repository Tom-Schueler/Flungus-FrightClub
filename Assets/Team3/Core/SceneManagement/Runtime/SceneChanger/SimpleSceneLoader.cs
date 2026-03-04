using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KekwDetlef.SceneManagement
{
    public class SimpleSceneLoader : SceneChanger
    {        
        [SerializeField] private SceneType sceneType;
        [SerializeField] private LoadSceneMode loadMode; 
        [SerializeField] private UISceneInfo uiSceneInfo;
        [SerializeField] private WorldSceneInfo worldSceneInfo;

        public UnityEvent OnSceneLoadStarted;
        public UnityEvent OnSceneLoadEnded;

#if UNITY_EDITOR
        public string NO_sceneType => nameof(sceneType);
        public string NO_loadMode => nameof(loadMode);
        public string NO_uiSceneInfo => nameof(uiSceneInfo);
        public string NO_worldSceneInfo => nameof(worldSceneInfo);
#endif

        public async void LoadScene()
        {
            await RetreveKey();

            OnSceneLoadStarted?.Invoke();

            AsyncOperationHandle<SceneInstance> handle;
            List<Task> loadTasks = new List<Task>();
            AssetReference sceneAsset;

            if (loadMode == LoadSceneMode.Single)
            {
                RuntimeSceneContainer.activeUISceneMap.Clear();
                RuntimeSceneContainer.activeWorldSceneMap.Clear();
            }

            if (sceneType == SceneType.UI && !RuntimeSceneContainer.activeUISceneMap.ContainsKey(uiSceneInfo.type))
            {
                sceneAsset = SceneMap.uiSceneMap[uiSceneInfo.type];
                handle = Addressables.LoadSceneAsync(sceneAsset, loadMode, true, uiSceneInfo.priority);
                RuntimeSceneContainer.activeUISceneMap.Add(uiSceneInfo.type, handle);

                loadTasks.Add(handle.Task);
            }

            if (sceneType == SceneType.World && !RuntimeSceneContainer.activeWorldSceneMap.ContainsKey(worldSceneInfo.type))
            {
                sceneAsset = SceneMap.worldSceneMap[worldSceneInfo.type];
                handle = Addressables.LoadSceneAsync(sceneAsset, loadMode, true, worldSceneInfo.priority);
                RuntimeSceneContainer.activeWorldSceneMap.Add(worldSceneInfo.type, handle);

                loadTasks.Add(handle.Task);
            }

            await Task.WhenAll(loadTasks);
            OnSceneLoadEnded?.Invoke();

            ReturnKey();
        }
    }
}
