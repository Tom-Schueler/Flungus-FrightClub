using System;
using UnityEngine;

namespace KekwDetlef.SceneManagement
{
    [Serializable]
    public class UISceneInfo
    {
        [SerializeField] public SceneMap.UIScene type;
        [SerializeField, Range(0, 100)] public int priority = 100;
        [SerializeField] public bool shouldReload;
    }

    [Serializable]
    public class WorldSceneInfo
    {
        [SerializeField] public SceneMap.WorldScene type;
        [SerializeField, Range(0, 100)] public int priority = 100;
        [SerializeField] public bool shouldReload;
    }
}

