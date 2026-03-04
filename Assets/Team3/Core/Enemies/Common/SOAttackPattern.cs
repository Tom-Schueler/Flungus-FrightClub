using Team3.Weapons;
using UnityEngine;

namespace Team3.Enemys.Common
{
    public class SOAttackData : ScriptableObject
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float stopDistance;
        [SerializeField]
        private float cooldown;
        [SerializeField]
        private float damage;
        [SerializeField]
        private DamageType damageType;
        [SerializeField]
        private float rotationSpeed;

        public float Cooldown => cooldown;
        public float Damage => damage;
        public DamageType DamageType => damageType;
        public float Speed => speed;
        public float StopDistance => stopDistance;
        public float RotationSpeed => rotationSpeed;
    }
}
