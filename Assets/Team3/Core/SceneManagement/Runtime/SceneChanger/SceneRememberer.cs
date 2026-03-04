using UnityEngine.Events;

namespace KekwDetlef.SceneManagement
{
    public class SceneRememberer : SceneChanger
    {
        public UnityEvent OnCached;

        public async void RememberSceneCompound()
        {
            await RetreveKey();

            RuntimeSceneContainer.rememberedUICompound.Clear();
            RuntimeSceneContainer.rememberedWorldCompound.Clear();
            
            foreach (var uiScene in RuntimeSceneContainer.activeUISceneMap)
            {
                RuntimeSceneContainer.rememberedUICompound.Add(uiScene.Key);
            }

            foreach (var worldScene in RuntimeSceneContainer.activeWorldSceneMap)
            {
                RuntimeSceneContainer.rememberedWorldCompound.Add(worldScene.Key);
            }

            ReturnKey();
            OnCached?.Invoke();
        }
    }
}
