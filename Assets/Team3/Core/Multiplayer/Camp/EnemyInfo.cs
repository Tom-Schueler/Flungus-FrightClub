using System;
using Team3.Enemys;
using UnityEngine;

namespace Team3.Multiplayer.EnemyCamp
{
    [Serializable]
    public class EnemyInfo
    {
        [SerializeField] private NetworkEnemy prefab;
        [SerializeField] private int amount;

        public NetworkEnemy Prefab => prefab;
        public int SpawnAmount => amount;
    }
}
