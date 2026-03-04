using Team3.Enemys.Common;
using UnityEngine;

namespace Team3.Enemys.MeeleEnemy
{
    [CreateAssetMenu(fileName = "Basic Melee Attack", menuName = "Team3/Enemy/Basic Melee Attack")]
    public class BasicMeleeAttackData : SOAttackData
    {
        [SerializeField]
        private float jumpForce;

        public float JumpForce => jumpForce;
    }
}
