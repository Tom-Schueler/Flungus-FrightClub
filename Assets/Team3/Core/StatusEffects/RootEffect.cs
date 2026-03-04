using Team3.Characters;
using UnityEngine;

namespace Team3.StatusEffect
{
    [CreateAssetMenu(fileName = "RootEffect", menuName = "Team3/Weapons/StatusEffects/RootEffect")]
    public class SORootEffect : SOStatusEffect
    {
        [SerializeField]
        private float duration;

        public override void Apply(CharacterStats target)
        {
            target.ApplyRoot(duration);
        }
    }
}