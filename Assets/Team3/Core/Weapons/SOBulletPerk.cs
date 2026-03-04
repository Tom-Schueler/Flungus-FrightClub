using Team3.Weapons;
using UnityEngine;
using Team3.CustomStructs;
using Team3.Characters;
namespace Team3.Weapons
{
    public abstract class SOBulletPerk :SOPerkModifier, IBulletPerk
    {
        public abstract void OnDestruction(BulletObject bullet);

        public abstract void OnImpact(BulletObject bullet, CharacterStats impactObject);

        public abstract void OnSpawn(BulletObject bullet);

        public abstract void OnUpdate(BulletObject bullet);
    }
}