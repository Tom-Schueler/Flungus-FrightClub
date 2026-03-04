using UnityEngine;
using Team3.CustomStructs;
using Team3.Characters;

namespace Team3.Skills
{
    public abstract class Skill : ScriptableObject, ISkill
    {
        [SerializeField]
        private DamageTypeValue damageTypeValue;

        [SerializeField]
        private float cooldown;
        [SerializeField]
        private float cooldownRemaining;


        public abstract void ActivateSkill(SkillContext skillContext, PlayerStats char_Stats, float damage);

        public float GetCooldownSecondsRemaining()
        {
            return cooldownRemaining;
        }

        public float GetCooldownPercentageRemaining()
        {
            return Mathf.Clamp01(cooldownRemaining / cooldown);
        }

        public float GetCooldownPercentagePassed()
        {
            return Mathf.Clamp01((cooldown - cooldownRemaining) / cooldown);
        }
    }

}