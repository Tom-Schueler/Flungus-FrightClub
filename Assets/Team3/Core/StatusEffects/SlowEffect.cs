using Team3.Characters;
using UnityEngine;

namespace Team3.StatusEffect
{
    [CreateAssetMenu(fileName = "SlowEffect", menuName = "Team3/Weapons/StatusEffects/SlowEffect")]
    public class SOSlowEffect : SOStatusEffect
    {
        [SerializeField]
        private float slowAmount;
        [SerializeField]
        private float duration;
        
        public override void Apply(CharacterStats target)
        {
            target.ApplySlow(slowAmount, duration);
        }
    }
}