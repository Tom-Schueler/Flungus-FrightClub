using System;
using UnityEngine.AddressableAssets;

namespace KekwDetlef.SceneManagement.Editor
{
    [Serializable]
    public struct IndexedScene
    {
        public AssetReference scene;
        public int sceneIndex;

        public IndexedScene(AssetReference scene, int sceneIndex)
        {
            this.scene = scene;
            this.sceneIndex = sceneIndex;
        }

        public override readonly string ToString()
        {
            return $"{sceneIndex}: '{scene.AssetGUID}'";
        }
    }
}
    
