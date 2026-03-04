using System;
using UnityEngine;

namespace Team3.Multiplayer.EnemyCamp
{
    [Serializable]
    public class EnemyWave
    {
        [SerializeField] private EnemyInfo[] enemys;

        public EnemyInfo[] Enemys => enemys;
    }
}
