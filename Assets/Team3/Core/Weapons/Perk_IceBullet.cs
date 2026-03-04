using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Team3.CustomStructs;
using Team3.Characters;

namespace Team3.Weapons
{
    [CreateAssetMenu(fileName = "IceBullet", menuName = "Team3/Weapons/BulletPerks/IceBullet")]
    public class Perk_IceBullet : SOBulletPerk
    {
        [SerializeField]
        private GameObject iceExplosionFX;

        [SerializeField]
        private float fxLifeTime;


        public override void OnDestruction(BulletObject bullet)
        {

        }

        public override void OnImpact(BulletObject bullet, CharacterStats impactObject)
        {
            GameObject iceFX = Instantiate(iceExplosionFX, bullet.transform.position, Quaternion.identity);
            //iceFX.GetComponent<DestroyObjectAfterTime>().lifeTime = fxLifeTime;
           
        }

        public override void OnSpawn(BulletObject bullet)
        {

        }

        public override void OnUpdate(BulletObject bullet)
        {
            
        }
    }
}
