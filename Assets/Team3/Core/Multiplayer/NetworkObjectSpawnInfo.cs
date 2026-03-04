using System;
using Unity.Netcode;
using UnityEngine;

namespace Team3.Multiplayer
{
    [Serializable]
    public struct NetworkObjectSpawnInfo
    {
        [SerializeField] public NetworkObject networkObject;
        [Tooltip("If you just want it to spawn at the root just leave blank. dont set the parent to be the spawner as it will destroy itslef after spawning all objcets"),
        SerializeField] public Transform parentTransform;
    }
}
