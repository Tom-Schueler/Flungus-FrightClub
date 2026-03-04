using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Team3.CustomStructs;
using Team3.Characters;

namespace Team3.Weapons
{
    [CreateAssetMenu(fileName = "MultiShot", menuName = "Team3/Weapons/BulletPerks/MultiShot")]
    public class Perk_MultiShot : SOBulletPerk
    {
        [SerializeField]
        private GameObject exploisionFX;

        [SerializeField]
        private int numberOfBulletsPerSplit;
        [SerializeField]
        private int numberOfSplits;
        [SerializeField]
        private float splitBulletLifetime;
        public override void OnDestruction(BulletObject bullet)
        {



        }

        public override void OnImpact(BulletObject bullet, CharacterStats impactObject)
        {

        }

        public override void OnSpawn(BulletObject bullet)
        {

            for (int i = 0; i < numberOfBulletsPerSplit; i++)
            {
                float angle = i * (360f / numberOfBulletsPerSplit);
                float rad = angle * Mathf.Deg2Rad;

                Vector3 direction = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad));


                GameObject splitBullet = Instantiate(bullet.gameObject, bullet.transform.position, Quaternion.LookRotation(direction));
                splitBullet.GetComponent<BulletObject>().lifeTime = splitBulletLifetime;
                splitBullet.GetComponent<BulletObject>().hitEnemies = bullet.hitEnemies;
                splitBullet.GetComponent<BulletObject>().isDestroyed = false;
                splitBullet.SetActive(true);

                splitBullet.GetComponent<BulletObject>().AddBulletVelocity();
            }

        }

        public override void OnUpdate(BulletObject bullet)
        {

        }
    }
}
