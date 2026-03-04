using Team3.Characters;
using UnityEngine;

namespace Team3.StatusEffect
{
    [CreateAssetMenu(fileName = "WaterEffect", menuName = "Team3/Weapons/StatusEffects/WaterEffect")]
    public class SOWaterEffect : SOStatusEffect
    {
        [SerializeField]
        private float duration;

        public override void Apply(CharacterStats target)
        {
            target.ApplyWater(duration,1);
        }
    }
}