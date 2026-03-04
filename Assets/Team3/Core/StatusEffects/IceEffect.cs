using Team3.Characters;
using UnityEngine;

namespace Team3.StatusEffect
{
    [CreateAssetMenu(fileName = "IceEffect", menuName = "Team3/Weapons/StatusEffects/IceEffect")]
    public class SOIceEffect : SOStatusEffect
    {
        [SerializeField]
        private float duration;
        [SerializeField]
        private int stackSize;

        public override void Apply(CharacterStats target)
        {
            target.ApplyIce(duration, stackSize);
        }
    }
}