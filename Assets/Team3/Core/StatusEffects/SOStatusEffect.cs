using System;
using Team3.Characters;
using UnityEngine;

namespace Team3.StatusEffect
{
    [Serializable]
    public abstract class SOStatusEffect : ScriptableObject, IStatusEffect
    {
        public abstract void Apply(CharacterStats target);
    }
}