using UnityEngine;
using Team3.Characters;


namespace Team3.Skills
{
    public interface ISkill
    {
        void ActivateSkill(SkillContext skillContext, PlayerStats char_Stats, float damage);

    }
}