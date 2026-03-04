using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Team3.CustomStructs;
using Team3.Characters; 

namespace Team3.Weapons
{
    [CreateAssetMenu(fileName = "HolyMoly", menuName = "Team3/Weapons/BulletPerks/HolyMoly")]
    public class Perk_HolyMoly : SOBulletPerk
    {
        [SerializeField]
        private GameObject holyFx;

        [SerializeField]
        private float triggerFxIntervals;

        [SerializeField]
        private float fxLifeTime;

        private float currentTime;

        public override void OnDestruction(BulletObject bullet)
        {
            
        }

        public override void OnImpact(BulletObject bullet, CharacterStats impactObject)
        {
           
        }

        public override void OnSpawn(BulletObject bullet)
        {
            //currentTime = triggerFxIntervals;
        }

        public override void OnUpdate(BulletObject bullet)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0) {

                GameObject holyfx = Instantiate(holyFx, bullet.transform.position, Quaternion.LookRotation(bullet.GetComponent<Rigidbody>().linearVelocity));
                holyfx.GetComponent<DestroyObjectAfterTime>().lifeTime = fxLifeTime;
                currentTime = triggerFxIntervals;
            }
        }
    }
}
