using System.Collections.Generic;
using Team3.StatusEffect;
using UnityEngine;

namespace Team3.Weapons
{
    [CreateAssetMenu(fileName = "Perk_Modifier", menuName = "Team3/Weapons/Perk_Modifier")]
    public class SOPerkModifier : ScriptableObject
    {
        [SerializeField] private float force;
        [SerializeField] private float gravityMultiplier;
        [SerializeField] private List<SOStatusEffect> statusEffects = new List<SOStatusEffect>();

        public float GravityMultiplier => gravityMultiplier;
        public float Force => force;
        public List<SOStatusEffect> StatusEffects => statusEffects;
    }
}