using System.Collections.Generic;
using UnityEngine;
using Team3.CustomStructs;
using Team3.Characters;

namespace Team3.Weapons
{
    public interface IBulletPerk
    {
        abstract void OnSpawn(BulletObject bullet);
        abstract void OnUpdate(BulletObject bullet);
        abstract void OnDestruction(BulletObject bullet);
        abstract void OnImpact(BulletObject bullet, CharacterStats impactObject);
    }
}