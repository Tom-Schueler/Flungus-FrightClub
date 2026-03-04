using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Team3.CustomStructs;
using Team3.Characters;




namespace Team3.Weapons
{
    public class BulletFactory : NetworkBehaviour
    {
    }

/*
        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private List<SOBulletPerk> perkList = new List<SOBulletPerk>();

        [SerializeField]
        private PlayerStats playerStats;

        [SerializeField]
        private SOBullet bulletData;

        [SerializeField]
        private List<DamageTypeValue> damagePercentage = new List<DamageTypeValue>();

        [SerializeField]
        private List<DamageTypeValue> damageFlat = new List<DamageTypeValue>();

        private float force, gravityMultiplier;

        private bool getsDestroyedOnImpact;
        private SOBullet mybullet;

        private GameObject bulletTemplate;
        private uint bulletVersion = 0;

        private float baseDamage;
        private int splitDamageBetweenBullets;

        public override void OnNetworkSpawn()
        {
            getsDestroyedOnImpact = bulletData.IsDestroyedOnImpact;
            damageFlat = bulletData.DamageTypeFlat;
            force = bulletData.Force;
            gravityMultiplier = bulletData.GravityMultiplier;
            baseDamage = this.gameObject.GetComponent<WeaponBase>().WeaponInfo.BaseDamage;
            splitDamageBetweenBullets = this.gameObject.GetComponent<WeaponBase>().WeaponInfo.SplitDamageBetweenBullets;


            if (perkList != null)
            {
                foreach (SOBulletPerk perk in perkList)
                {
                    // damageFlat = MergeDamageStats(damageFlat, perk.DamageTypeFlat);
                    // damagePercentage = MergeDamagePercentage(damagePercentage, perk.DamageTypePercentage);



                    force += perk.Force;
                    gravityMultiplier = Mathf.Clamp(gravityMultiplier += perk.GravityMultiplier, 0f, 20f);
                    //damageTypes |= perk.DamageType;
                }
            }

            bulletTemplate = Instantiate(bulletPrefab, transform);
            bulletTemplate.SetActive(false);
            SetBulletTemplateData();

        }

        private void SetBulletTemplateData()
        {


            BulletObject templateData = bulletTemplate.gameObject.GetComponent<BulletObject>();
            
            if (templateData != null)
            {
                templateData.force = force;
                templateData.gravityMultiplier = gravityMultiplier;
                templateData.lifeTime = bulletData.LifeTime;
                templateData.collisionRadius = bulletData.CollisionRadius;
                templateData.projectileMesh = bulletData.ProjectileMesh;
                templateData.isDestroyedOnImpact = bulletData.IsDestroyedOnImpact;

                templateData.impactFX = bulletData.ImpactFX;
                templateData.spawnFX = bulletData.SpawnFX;
                templateData.trailFX = bulletData.TrailFx;


                templateData.numberOfAncestors = 0;


                templateData.type = playerStats.currentBulletType;
                templateData.affix = playerStats.currentBulletAffix;

                templateData.damageTypeDamage = playerStats.currentBulletDamage;
                templateData.perkList = playerStats.collectedBulletsPerk;
                Debug.Log("Base Damage"+ baseDamage);
                Debug.Log("Split Damage"+ splitDamageBetweenBullets);
                Debug.Log("DPB " + baseDamage / splitDamageBetweenBullets);
                templateData.damagePerBullet = baseDamage / splitDamageBetweenBullets;
                templateData.damage = calculateBulletDamage();
                
            }
        }
      
        public void UpdateBulletData()
        {
            if (playerStats.version != bulletVersion)
            {
                BulletObject templateData = bulletTemplate.gameObject.GetComponent<BulletObject>();

                //####### Relevant Damage Types Updated #######
                templateData.type = playerStats.currentBulletType;
                templateData.affix = playerStats.currentBulletAffix;

                //######## Relevant Lists Updated #######
                templateData.damageTypeDamage = playerStats.currentBulletDamage;
                templateData.perkList = playerStats.collectedBulletsPerk;
                templateData.damage = calculateBulletDamage();
              
                templateData.damagePerBullet = templateData.damage / splitDamageBetweenBullets;
            }
        }

        public float calculateBulletDamage()
        {
            float dmg = 0;

            foreach (var entry in playerStats.currentBulletDamage)
            {
                if (entry.type == playerStats.currentBulletType)
                {
                    dmg = entry.value;

                    break;
                }

            }

            
            if (baseDamage / splitDamageBetweenBullets < baseDamage){dmg += baseDamage / splitDamageBetweenBullets; }
            else { dmg += baseDamage; }
                
            foreach (var entry in playerStats.currentBulletDamage)
            {
                if (entry.type == playerStats.currentBulletAffix)
                {
                    dmg += entry.value;

                    break;
                }

            }

            return dmg;

        }
        public void SpawnBullet(Vector3 position, Quaternion direction,int splitDamageBetweenBullets)
        {
            SpawnBulletServerRpc(position,direction, splitDamageBetweenBullets);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnBulletServerRpc(Vector3 position, Quaternion direction, int splitDamageBetweenBullets)
        {
            this.splitDamageBetweenBullets = splitDamageBetweenBullets;
            UpdateBulletData();
            GameObject bullet = Instantiate(bulletTemplate, position, direction);

            bullet.GetComponent<BulletObject>().ownerObject = this.gameObject;
            bullet.SetActive(true);
            bullet.GetComponent<NetworkObject>().Spawn(true);
            bullet.GetComponent<BulletObject>().OnSpawn();
            
        }
    }
}

    */ 
}