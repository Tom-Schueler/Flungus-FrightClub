using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using Team3.Skills;
using Team3.Characters;
using static UnityEngine.InputSystem.InputAction;
using Unity.Netcode;
namespace Team3.Weapons
{
    public class WeaponBase : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private SOWeapon weaponInfo;

        private int bulletsLeft;
        private int bulletsShot;
        private Transform bulletSpawn;
        private Animation weaponAnimation;

        [SerializeField]
        private Camera fpsCam;

        private WeaponState weaponState;
        private bool allowInvoke = true;

        private GameObject currentWeapon;
        public GameObject CurrentWeapon => currentWeapon;
        public WeaponState WeaponState => weaponState;
        public int BulletsLeft => bulletsLeft;
        public int BulletsShot => bulletsShot;
        public SOWeapon WeaponInfo => weaponInfo;

        public int original = 0;
        public void Initialize(SOWeapon weaponData = null)
        {
            if (weaponData != null)
            {
                weaponInfo = weaponData;
            }

            AttachWeapon();
            weaponAnimation = CurrentWeapon.GetComponentInChildren<Animation>();
            bulletSpawn = currentWeapon.transform.Find("BulletSpawn");
            bulletsLeft = WeaponInfo.MagazineSize;
            weaponState = WeaponState.Ready;

            weaponAnimation.AddClip(weaponInfo.ShootAnimation, "Shoot");
            weaponAnimation.AddClip(weaponInfo.ReloadAnimation, "Reload");


        }

        private void Start()
        {
            Initialize(weaponInfo);
            Debug.Log("INIT");
        }


        public void Reload(PlayerStats char_Stats)
        {
            if (bulletsLeft < WeaponInfo.MagazineSize && weaponState != WeaponState.Reloading)
            {

                Debug.Log("KeyReload");
                weaponState = WeaponState.Reloading;
                weaponAnimation.Play("Reload");

                //Debug.Log("ReloadStarted");
                CancelInvoke("ReloadFinished");
                Invoke("ReloadFinished", weaponInfo.ReloadTime);
            }

        }


        private void ReloadFinished()
        {
            bulletsLeft = weaponInfo.MagazineSize;
            weaponState = WeaponState.Ready;
            //Debug.Log("ReloadFinished");
        }

        public void Shoot(PlayerStats char_Stats, CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {

                if (weaponState == WeaponState.Ready && callbackContext.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction && WeaponInfo.HoldDownToShoot)
                {

                    Debug.Log("Hold");

                    weaponState = WeaponState.Shooting;

                }

                else if (weaponState == WeaponState.Ready && callbackContext.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
                {
                    Debug.Log("Tapped");
                    weaponState = WeaponState.Shooting;
                }

                else
                {
                    if (weaponState == WeaponState.Ready)
                    {
                        Debug.Log("Pressed");
                        weaponState = WeaponState.Shooting;
                    }

                    else if (weaponState != WeaponState.Reloading && bulletsLeft <= 0)
                    {
                        //Debug.Log("ShootReload");
                        weaponState = WeaponState.Reloading;


                        {

                            //Debug.Log("KeyReload");
                            weaponState = WeaponState.Reloading;
                            weaponAnimation.Play("Reload");

                            //Debug.Log("ReloadStarted");
                            CancelInvoke("ReloadFinished");
                            Invoke("ReloadFinished", weaponInfo.ReloadTime);

                        }

                    }
                }


                if (weaponState == WeaponState.Shooting && bulletsLeft > 0)
                {
                    weaponState = WeaponState.None;
                    bulletsShot = 0;
                    original = 0;
                    Shooting();
                }
            }
        }


        public void Shooting()
        {
            //Shoot Animation
            weaponAnimation.Play("Shoot");


            //Get Point in Front either if something is hit or on a max distance
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100);
            }



            //Calculate direction
            Vector3 directionWithoutSpread = ray.GetPoint(100) - bulletSpawn.position;



            //calcualte spread
            float x = Random.Range(-weaponInfo.Spread, weaponInfo.Spread);
            float y = Random.Range(-weaponInfo.Spread, weaponInfo.Spread);
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            //Spawn Bullet with Factory
            //gameObject.GetComponent<BulletFactory>().SpawnBullet(bulletSpawn.position,
            //                                                       Quaternion.LookRotation(directionWithSpread.normalized),
            //                                                      weaponInfo.SplitDamageBetweenBullets);

            bulletsLeft = Mathf.Clamp(bulletsLeft, 0, weaponInfo.MagazineSize);
            bulletsShot++;

            if (allowInvoke)
            {
                bulletsLeft--;
                //Invoke Reset Shot
                Invoke("ResetShot", weaponInfo.FireRate);
                allowInvoke = false;

            }

            if (bulletsShot < weaponInfo.BulletsPerClick)
            {
                original = -1;
                Invoke("Shooting", weaponInfo.TimeBetweenBullets); //weaponInfo.FireRate time between bullets
            }
        }

        private void ResetShot()
        {
            //Debug.Log("RESETTED");
            weaponState = WeaponState.Ready;
            allowInvoke = true;
        }

        public void Update()
        {

        }


        void AttachWeapon()
        {
            if (weaponInfo.Asset == null) return;

            if (currentWeapon != null)
            {
                currentWeapon.GetComponent<NetworkObject>().Despawn();
            }

            GameObject weaponInstance = Instantiate(weaponInfo.Asset, transform);
            currentWeapon = weaponInstance;

            AttachWeaponServerRpc();
            Debug.Log("Hello");
        }




        [ServerRpc]
        public void AttachWeaponServerRpc()
        {
            GameObject weaponInstance = Instantiate(weaponInfo.Asset, transform);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;
            weaponInstance.GetComponent<NetworkObject>().Spawn();
        }
    }

}