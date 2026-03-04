using Team3.Enemys.Common;
using UnityEngine;

namespace Team3.Enemys.RangedEnemy
{
    [CreateAssetMenu(fileName = "Basic Range Attack", menuName = "Team3/Enemy/Basic Range Attack")]
    public class BasicRangeAttackData : SOAttackData
    {
        [SerializeField]
        private GameObject projectile;
        public GameObject Projectile => projectile;
    }
}
