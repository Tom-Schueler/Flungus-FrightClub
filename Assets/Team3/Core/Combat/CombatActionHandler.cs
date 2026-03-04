using System.Collections.Generic;
using Team3.Characters;
using Unity.Netcode;
using UnityEngine;
using Team3.Weapons;
using static UnityEngine.InputSystem.InputAction;

using UnityEngine.Pool;

using Team3.Enemys.Common;
using UnityEngine.UIElements;
using System.Linq;



namespace Team3.Combat
{
    public class CombatActionHandler : NetworkBehaviour
    {
        [SerializeField]
        private GameObject serverBulletPrefab;
        [SerializeField]
        private GameObject clientBulletPrefab;

        private GameObject serverBulletTemplate;
        private GameObject clientBulletTemplate;
        [SerializeField] private AudioSource audio;
        [SerializeField] private CameraShake shake;
        [SerializeField]
        private PlayerStats playerStats;
        [SerializeField]
        private Camera fpsCam;
        [SerializeField]
        private GameObject weaponSocket;
        [SerializeField]
        private GameObject skillSocket;
        [SerializeField]
        private SOGun gun;
        [SerializeField]
        private DamageType attackDamageType;
        [SerializeField]
        private DamageType skillDamageType;
        [SerializeField]
        private List<SOProjectilePerk> onShootPerks = new List<SOProjectilePerk>();
        public SOGun RuntimeGun => PerkDatabase.Instance.GetGunByID(gunID.Value);
        private bool isReady = false;

        [SerializeField]
        private uint bulletVersion = 420;

        private bool allowInvoke = true;
        private bool isAttackPressed = false;
        private int bulletsPerClick = 1;

        [SerializeField] private BulletTraces bulletTraceObject;
        private IObjectPool<BulletTraces> bulletTracePool;

        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxCapacity = 50;

        [SerializeField] private MuzzleFlash muzzleFlashObject;
        private IObjectPool<MuzzleFlash> muzzleFlashPool;

        [SerializeField] private MuzzleFlash impactFlashObject;
        private IObjectPool<MuzzleFlash> impactFlashPool;

        // #################  WEAPON STUFF  #######################
        private WeaponState weaponState = WeaponState.Ready;
        private int bulletsLeft;
        private int bulletsShot;
        private float DamageScale;

        [SerializeField]
        private GameObject gunPrefab;
        private Animation weaponAnimation;
        private Transform bulletSpawn;
        private int magazineSize;
        private bool triggerPerks;



        [SerializeField]
        private float rotationSpeed = 10f;

        private NetworkVariable<int> gunID = new NetworkVariable<int>();

        public PlayerStats PlayerStats => playerStats;

        [SerializeField] private LayerMask targetLayers;

        [SerializeField] private Material[] playerMaterial;
        [SerializeField] private GameObject playerCharacter;
        [SerializeField] private GameObject playerModel;





        public override void OnNetworkSpawn()
        {
            playerCharacter.GetComponent<SkinnedMeshRenderer>().material = playerMaterial[(int)OwnerClientId]; //playerMaterial[(int)OwnerClientId]

            if (IsServer)
            {
                gunID.Value = 4 - (int)OwnerClientId;
                AttachGunToPlayerClientRpc();
            }

            if (IsOwner)
            {

            }
        }



        [ClientRpc]
        public void AttachGunToPlayerClientRpc()
        {
            var instantiatedItem = Instantiate(RuntimeGun.GunAsset);
            instantiatedItem.transform.SetParent(weaponSocket.transform);
            instantiatedItem.transform.localPosition = Vector3.zero;
            instantiatedItem.transform.localRotation = Quaternion.identity;

            if (!IsOwner) return;

            gunPrefab = instantiatedItem.gameObject;
            SetupGun();
            gunPrefab.layer = 11;
            
            var children = gunPrefab.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
            children.ForEach(c => c.layer = 11);

            playerModel.layer = 13;
            children = playerModel.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
            children.ForEach(c => c.layer = 13);

            weaponAnimation = gunPrefab.GetComponentInChildren<Animation>();
            //Debug.LogError(weaponAnimation);
            //weaponAnimation.AddClip(RuntimeGun.ShootAnimation, "Shoot");
           //weaponAnimation.AddClip(RuntimeGun.ReloadAnimation, "Reload");
        }



        public void SetupGun()
        {
            bulletsPerClick = RuntimeGun.BulletsPerClick;
            bulletsLeft = RuntimeGun.MagazineSize;
            magazineSize = RuntimeGun.MagazineSize;
            bulletSpawn = gunPrefab.transform.Find("BulletSpawn");

            

        }



        public void Start()
        {
            //SetupServerRpc();
            playerStats.AddPerk(RuntimeGun.CharacterCard.ID);

            //UpdateBulletDamageData();
            bulletTracePool = new ObjectPool<BulletTraces>(CreateTrace, OnGetTraceFromPool, OnReleaseTraceToPool, OnDestroyTracePooledObject, false, defaultCapacity, maxCapacity);
            muzzleFlashPool = new ObjectPool<MuzzleFlash>(CreateMuzzleFlash, OnGetMuzzleFromPool, OnReleaseMuzzleToPool, OnDestroyMuzzlePooledObject, false, defaultCapacity, maxCapacity);
            impactFlashPool = new ObjectPool<MuzzleFlash>(CreateImpactFlash, OnGetMuzzleFromPool, OnReleaseMuzzleToPool, OnDestroyMuzzlePooledObject, false, defaultCapacity, maxCapacity);


        }


        private BulletTraces CreateTrace()
        {
            BulletTraces trace = Instantiate(bulletTraceObject);
            trace.BulletTracePool = bulletTracePool;
            return trace;
        }

        private void OnGetTraceFromPool(BulletTraces trace) { trace.gameObject.SetActive(true); }
        private void OnReleaseTraceToPool(BulletTraces trace) { trace.gameObject.SetActive(false); }
        private void OnDestroyTracePooledObject(BulletTraces trace) { Destroy(trace.gameObject); }


        private MuzzleFlash CreateMuzzleFlash()
        {
            MuzzleFlash flash = Instantiate(muzzleFlashObject);
            flash.MuzzleFlashPool = muzzleFlashPool;
            return flash;
        }

        private MuzzleFlash CreateImpactFlash()
        {
            MuzzleFlash flash = Instantiate(impactFlashObject);
            flash.MuzzleFlashPool = impactFlashPool;
            return flash;
        }



        private void OnGetMuzzleFromPool(MuzzleFlash flash) { flash.gameObject.SetActive(true); }
        private void OnReleaseMuzzleToPool(MuzzleFlash flash) { flash.gameObject.SetActive(false); }
        private void OnDestroyMuzzlePooledObject(MuzzleFlash flash) { Destroy(flash.gameObject); }



        public void InputShootAction(CallbackContext callbackContext)
        {

            if (callbackContext.performed)
            {
                isAttackPressed = true;

                if (weaponState != WeaponState.Reloading && bulletsLeft <= 0)
                {
                    weaponState = WeaponState.Reloading;

                    //weaponAnimation.Play("Reload");
                    CancelInvoke("ReloadFinished");
                    Invoke("ReloadFinished", RuntimeGun.ReloadTime);
                }

                if (weaponState == WeaponState.Ready)
                {
                    weaponState = WeaponState.Shooting;
                }



                if (weaponState == WeaponState.Shooting && bulletsLeft > 0)
                {
                    weaponState = WeaponState.None;
                    bulletsShot = 0;
                    Shooting();
                }
            }

            if (callbackContext.canceled)
            {
                isAttackPressed = false;
            }
        }


        public void Update()
        {
            if (isAttackPressed)
            {
                if (weaponState != WeaponState.Reloading && bulletsLeft <= 0)
                {

                    weaponState = WeaponState.Reloading;
                    //weaponAnimation.Play("Reload");
                    CancelInvoke("ReloadFinished");
                    Invoke("ReloadFinished", RuntimeGun.ReloadTime);
                }

                if (weaponState == WeaponState.Ready)
                {
                    weaponState = WeaponState.Shooting;
                }




                if (weaponState == WeaponState.Shooting && bulletsLeft > 0)
                {
                    weaponState = WeaponState.None;
                    bulletsShot = 0;
                    Shooting();
                }

            }
        }


        private void ReloadFinished()
        {
            bulletsLeft = magazineSize;
            weaponState = WeaponState.Ready;
        }

        public void IncreaseMagSize(float magSizeModifier)
        {
            magazineSize = (int)((1 + magSizeModifier) * RuntimeGun.MagazineSize);
        }



        //##########################################################################################################################################
        //##################################################| HITSCAN SHOOTING |####################################################################
        //##########################################################################################################################################


        [ServerRpc]
        public void TakeDamageServerRpc(ulong id, float damage, DamageType type, float saltmodifier)
        {
            PlayerRegistry.GetStats(id).TakeDamage(damage, type, 1, saltmodifier);
        }

        [ServerRpc]
        public void TriggerPerkServerRpc(int id, Vector3 pos, Vector3 dir, NetworkObjectReference hitRef = default)
        {
            PerkDatabase.Instance.GetPerkByID(id).TriggerPerk(null, 420, pos, dir, hitRef);

        }

        [ServerRpc]
        public void TriggerDeathPerkServerRpc(int id, Vector3 pos, Vector3 dir, NetworkObjectReference hitRef = default)
        {
            PerkDatabase.Instance.GetPerkByID(id).TriggerDeathPerk(null, 420, pos, dir, hitRef);

        }

        [ServerRpc]
        public void EnemytakeDamageServerRpc(NetworkObjectReference hitRef, float damage, DamageType type, float saltmodifier)
        {
            if (hitRef.TryGet(out NetworkObject hitObject))
            {
                Debug.LogError("hello");
                // Now try to get the component from the resolved object
                if (hitObject.TryGetComponent<EnemyStats>(out var stats)) { 
                }
                else { 
                   
                            stats = hitObject.gameObject.GetComponentInChildren<EnemyStats>();
                }

                stats.TakeDamage(damage * saltmodifier, type, 1, saltmodifier);
            }
        }

        public void Shooting()
        {
            //Shoot Animation
            //weaponAnimation.Play("Shoot");


            var ownerStats = PlayerRegistry.GetStats(OwnerClientId);

            //calcualte spread
            float x = UnityEngine.Random.Range(-RuntimeGun.Spread, RuntimeGun.Spread);
            float y = UnityEngine.Random.Range(-RuntimeGun.Spread, RuntimeGun.Spread);

            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 targetPoint;


            MuzzleFlash flash = muzzleFlashPool.Get();
            flash.transform.position = bulletSpawn.transform.position;
            flash.transform.rotation = Quaternion.LookRotation(bulletSpawn.forward);
            flash.transform.parent = bulletSpawn.transform;
            flash.Deactivate();

            shake.start = true;


            if (Physics.Raycast(ray, out RaycastHit hit, 100, targetLayers))
            {
                targetPoint = hit.point;


                if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Enemy"))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {

                        TakeDamageServerRpc(
                                    hit.collider.gameObject.GetComponent<NetworkObject>().OwnerClientId,
                                    ownerStats.BulletDamage * DamageScale,
                                    ownerStats.BulletDamageType,
                                    ownerStats.SaltDamageModifier); // calculateBulletDamage(PlayerRegistry.GetStats(OwnerClientId),1)
                    }

                    else
                    {


                        if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var hitRef))
                        {
                            EnemytakeDamageServerRpc(
                                    hitRef,
                                    ownerStats.BulletDamage * DamageScale,
                                    ownerStats.BulletDamageType,
                                    ownerStats.SaltDamageModifier);

                            foreach (var perk in ownerStats.EnemyDeathPerks)
                            {
                                if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
                                    TriggerDeathPerkServerRpc(perk.ID, hit.point, hit.normal, enemyRef);
                            }
                        }

                        else if ((hit.collider.GetComponentInParent<NetworkObject>()))
                        {
                            Debug.LogError("SMASH");

                            EnemytakeDamageServerRpc(
                                   hitRef,
                                   ownerStats.BulletDamage * DamageScale,
                                   ownerStats.BulletDamageType,
                                   ownerStats.SaltDamageModifier);

                            foreach (var perk in ownerStats.EnemyDeathPerks)
                            {
                                if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
                                    TriggerDeathPerkServerRpc(perk.ID, hit.point, hit.normal, enemyRef);
                            }
                        }

                    }


                    foreach (var perk in ownerStats.OnHitPerks)
                    {
                        if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var hitRef))
                            TriggerPerkServerRpc(perk.ID, hit.point, hit.normal, hitRef);
                    }


                }

                else
                {
                    MuzzleFlash impact = impactFlashPool.Get();
                    impact.transform.position = hit.point;
                    impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                    impact.Deactivate();

                    foreach (var perk in ownerStats.OnImpactPerks)
                    {

                        TriggerPerkServerRpc(perk.ID, hit.point, hit.normal);
                    }

                    ImpactFlashServerRpc(hit.point, hit.normal);

                }



            }
            else
            {
                targetPoint = ray.GetPoint(100); // Default far point
            }

     

            audio.clip = PerkDatabase.Instance.GetSFXByID(1).SFX;
            audio.Play();

            MuzzleFlashServerRpc(bulletSpawn.transform.position, bulletSpawn.forward);

            BulletTraces trace = bulletTracePool.Get();
            trace.SetPoints(bulletSpawn.position + bulletSpawn.forward * 2, targetPoint);
            trace.transform.rotation = Quaternion.LookRotation(bulletSpawn.forward * -1);
            trace.Deactivate();

            BulletTraceServerRpc(bulletSpawn.position, targetPoint, bulletSpawn.forward * -1);

            new Vector3(x, y, 0);

            bulletsLeft = Mathf.Clamp(bulletsLeft, 0, magazineSize);
            bulletsShot++;
            bulletsLeft--;


            if (allowInvoke)
            {
                //Invoke Reset Shot
                Invoke("ResetShot", RuntimeGun.FireRate);
                allowInvoke = false;
                foreach (var perk in onShootPerks)
                {
                    perk.TriggerPerk(null, 420, bulletSpawn.transform.position);
                }
            }

            if (bulletsShot < RuntimeGun.BulletsPerClick)
            {
                if (!RuntimeGun.SplitBulletTriggersPerk)
                {
                    triggerPerks = false;
                }
                Invoke("Shooting", RuntimeGun.TimeBetweenBullets);
            }
        }

        private void ResetShot()
        {

            weaponState = WeaponState.Ready;
            triggerPerks = true;
            allowInvoke = true;
        }


        public void FixedUpdate()
        {
            if (IsOwner)
                RotateWeaponToRayHitPoint();
        }

        public void RotateWeaponToRayHitPoint()
        {

            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out RaycastHit hit, 100, targetLayers))
            {

                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100); // Default far point
            }

            // Calculate the target direction from the weaponSocket to the target point
            Vector3 targetDirection = (targetPoint - weaponSocket.transform.position).normalized;

            // Smoothly rotate towards the target direction using Slerp
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            weaponSocket.transform.rotation = Quaternion.Slerp(weaponSocket.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }





        [ServerRpc]
        public void ImpactFlashServerRpc(Vector3 position, Vector3 rotation)
        {
            ImpactFlashClientRpc(position, rotation);
        }


        [ClientRpc]
        public void ImpactFlashClientRpc(Vector3 position, Vector3 rotation)
        {
            if (IsOwner) return;
            MuzzleFlash impact = impactFlashPool.Get();
            impact.transform.position = position;
            impact.transform.rotation = Quaternion.LookRotation(rotation);
            impact.Deactivate();
        }

        [ServerRpc]
        public void MuzzleFlashServerRpc(Vector3 position, Vector3 rotation)
        {
            MuzzleFlashClientRpc(position, rotation);
        }

        [ClientRpc]
        public void MuzzleFlashClientRpc(Vector3 position, Vector3 rotation)
        {

            if (IsOwner) return;
            MuzzleFlash flash = muzzleFlashPool.Get();
            flash.transform.position = position;
            flash.transform.rotation = Quaternion.LookRotation(rotation);
            flash.transform.parent = gameObject.transform;
            flash.Deactivate();
            AudioSource.PlayClipAtPoint(PerkDatabase.Instance.GetSFXByID(1).SFX, position);


        }


        [ServerRpc]
        public void BulletTraceServerRpc(Vector3 position, Vector3 positionHit, Vector3 rotation)
        {
            BulletTraceClientRpc(position, positionHit, rotation);
        }

        [ClientRpc]
        public void BulletTraceClientRpc(Vector3 position, Vector3 positionHit, Vector3 rotation)
        {
            if (IsOwner) return;
            BulletTraces trace = bulletTracePool.Get();
            trace.SetPoints(position, positionHit);
            trace.transform.rotation = Quaternion.LookRotation(rotation * -1);
            trace.Deactivate();
        }
    }
}
