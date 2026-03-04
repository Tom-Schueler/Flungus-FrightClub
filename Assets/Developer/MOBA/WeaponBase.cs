using Team3.Characters;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;
using Team3.Combat;


namespace Team3.MOBA
{
    public abstract class WeaponBase : NetworkBehaviour , IWeapon
    {
        [SerializeField] protected SOGun gunData;
        [SerializeField] protected Transform cameraSocket;
        [SerializeField] protected Transform bulletSpawn;
        [SerializeField] protected Recoil recoil;
        [SerializeField] protected PlayerStats ownerStats;
        [SerializeField] protected Camera fpsCamera;

        [SerializeField] protected WeaponState weaponState;
        [SerializeField] public bool isAttackPressed;
        [SerializeField] protected int bulletsLeft;
        [SerializeField] protected int bulletsShot;
        [SerializeField] protected bool triggerPerks = true;
        [SerializeField] protected bool allowInvoke = true;

        [SerializeField] protected LayerMask rayTargetLayers;

        [SerializeField] private CameraShake shake;
        [SerializeField] public SOCombatCards GunCard;
        [SerializeField] protected SOSFX hitMarkerSound; 
        [SerializeField] protected SOSFX hitMarkerKillSound;

        public int BulletsLeft => bulletsLeft;
        public int MagazineSize;




        public virtual void Start() 
        {
            MagazineSize = (int)(gunData.MagazineSize * ownerStats.magSizeModifier);
        }
        public virtual void Reload() 
        {
        
        }

        public void ForceReload()
        {
            ReloadFinished();
        }

        public virtual void SetBulletElement(Weapons.DamageType elementalType)
        {
           
        }

        public virtual void Shoot(Vector3 hitPoint, bool hasHit,bool triggerPerks, RaycastHit hitTarget = default)
        {

        }

        public virtual void InputReloadAction(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                if (weaponState != WeaponState.Reloading && weaponState == WeaponState.Ready && bulletsLeft < (int)(gunData.MagazineSize * ownerStats.magSizeModifier))
                {
                    weaponState = WeaponState.Reloading;
                    CancelInvoke("ReloadFinished");
                    CancelInvoke("ResetShot");
                    Invoke("ReloadFinished", gunData.ReloadTime);
                    Reload();
                }
            }
        }
        public virtual void InputShootAction(CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                isAttackPressed = true;

                //if (weaponState != WeaponState.Reloading && bulletsLeft <= 0)
                //{
                //    weaponState = WeaponState.Reloading;

                //    CancelInvoke("ReloadFinished");
                //    CancelInvoke("ResetShot");
                //    Reload();
                //    Invoke("ReloadFinished", gunData.ReloadTime);
                //}

                if (weaponState == WeaponState.Ready && bulletsLeft > 0)
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
                if (weaponState != WeaponState.Reloading && weaponState == WeaponState.Ready && bulletsLeft <= 0)
                {
                    Debug.LogError("Reload 3");
                    weaponState = WeaponState.Reloading;
                   
                    CancelInvoke("ReloadFinished");
                    CancelInvoke("ResetShot");
                    Invoke("ReloadFinished", gunData.ReloadTime);
                    Reload();
                }

                if (weaponState == WeaponState.Ready && bulletsLeft > 0)
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
            ownerStats = PlayerRegistry.GetStats(OwnerClientId);
            bulletsLeft = (int)(gunData.MagazineSize * ownerStats.magSizeModifier);
            weaponState = WeaponState.Ready;
            triggerPerks = true;
            allowInvoke = true;
        }


        public void Shooting()
        {
            

            //calcualte spread
            float x = UnityEngine.Random.Range(-gunData.Spread, gunData.Spread);
            float y = UnityEngine.Random.Range(-gunData.Spread, gunData.Spread);

            var spread = new Vector3(x, y, 0);

            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f+ spread.x, 0.5f+ spread.y, 0));
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out RaycastHit hit, 100, rayTargetLayers))
            {
                targetPoint = hit.point;
                Shoot(targetPoint,true,triggerPerks,hit);
            }
            else
            {
                targetPoint = ray.GetPoint(100); // Default far point
                Shoot(targetPoint, false, triggerPerks);
            }

            PlayMuzzleFlashEffect();
            NotifyWeaponFiredServerRpc(OwnerClientId);
            recoil.RecoilFire();
            shake.start = true;



            bulletsLeft = Mathf.Clamp(bulletsLeft, 0, (int)(gunData.MagazineSize * ownerStats.magSizeModifier));
            bulletsShot++;
            bulletsLeft--;


            if (allowInvoke)
            {
                allowInvoke = false;

                int index = 0;
                foreach (var perk in ownerStats.OnShootPerks)
                {
                    if (Time.time - ownerStats.OnShootPerksTrackCooldown[index] > perk.CoolDown)
                    {
                        perk.ClientTrigger(bulletSpawn.position, bulletSpawn.forward, OwnerClientId);
                        ownerStats.OnShootPerksTrackCooldown[index] = Time.time;
                    }
                    index++;
                }

                //Invoke Reset Shot
                Invoke("ResetShot", gunData.FireRate);
                
            }

            if (bulletsShot < gunData.BulletsPerClick)
            {
             triggerPerks = false;
              Invoke("Shooting", gunData.TimeBetweenBullets);
            }
        }

        private void ResetShot()
        {

            weaponState = WeaponState.Ready;
            triggerPerks = true;
            allowInvoke = true;
        }

        public void SetupWeapon(Transform cameraSocket, Recoil recoil, Camera fpsCamera, ulong ownerID,CameraShake shake)
        {
            this.cameraSocket = cameraSocket;
            this.recoil = recoil;
            this.fpsCamera = fpsCamera;
            this.shake = shake;

            ownerStats = PlayerRegistry.GetStats(ownerID);
            bulletsLeft = (int)(gunData.MagazineSize * ownerStats.magSizeModifier);
            
        }


        public virtual void PlayMuzzleFlashEffect()
        {
           
            GameObject fx = ObjectPoolManager.Instance.GetPooledObject(gunData.MuzzleFlashFx.ID);

            if (IsOwner)
            {
                fx.layer = 11;
                var children = fx.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
                children.ForEach(c => c.layer = 11);
            }

            fx.transform.SetParent(bulletSpawn, false); 
            fx.transform.localPosition = Vector3.zero;
            fx.transform.localRotation = Quaternion.identity;

            fx.GetComponent<Pitcher>()?.ChangePitch();
        }


        [ServerRpc(RequireOwnership = false)]
        public void NotifyWeaponFiredServerRpc(ulong id, ServerRpcParams rpcParams = default)
        {
            var shooter = rpcParams.Receive.SenderClientId;
            Debug.Log("Shooter id " + id + " is owner: " + IsOwner +" is server " + IsServer);
            TriggerMuzzleFlashClientRpc(id);
        }


        [ClientRpc]
        void TriggerMuzzleFlashClientRpc(ulong shooterClientId)
        {
            // Skip local shooter � already played VFX
            if (shooterClientId == NetworkManager.Singleton.LocalClientId)
            {
                Debug.Log("Shooter id " + shooterClientId +  " localid " + NetworkManager.Singleton.LocalClientId + " is owner: " + IsOwner + " is server " + IsServer);
                return;
            }
            Debug.Log("Ich bins id " + shooterClientId + " is owner: " + IsOwner + " is server " + IsServer);
            // Get the shooter's weapon
           
            Debug.LogError("Ello");
            PlayMuzzleFlashEffect();
        }




        //public void SpawnMuzzleFlash()
        //{
        //    var id = gunData.MuzzleFlashFx.ID;


        //    GameObject fx = ObjectPoolManager.Instance.GetPooledObject(id);
        //    fx.layer = 11;
        //    var children = fx.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
        //    children.ForEach(c => c.layer = 11);

        //    fx.transform.position = bulletSpawn.position;
        //    fx.transform.forward = bulletSpawn.forward;
        //    fx.transform.parent = bulletSpawn.transform;

        //    fx.GetComponent<Pitcher>().ChangePitch();


        //    RequestClientMuzzleFlashServerRpc(id, bulletSpawn.position, bulletSpawn.forward);

        //}

        //[ServerRpc(RequireOwnership = false)]
        //public void RequestClientMuzzleFlashServerRpc(int ID, Vector3 pos, Vector3 forward, ServerRpcParams rpcParams = default)
        //{
        //    var senderClientId = rpcParams.Receive.SenderClientId;

        //    MuzzleFlashClientRpc(ID, pos, forward, new ClientRpcParams
        //    {
        //        Send = new ClientRpcSendParams
        //        {
        //            TargetClientIds = NetworkManager.Singleton.ConnectedClientsIds
        //                .Where(id => id != senderClientId).ToArray()
        //        }
        //    });

        //}

        //[ClientRpc]
        //public void MuzzleFlashClientRpc(int ID, Vector3 pos, Vector3 forward, ClientRpcParams rpcParams = default)
        //{

        //    GameObject fx = ObjectPoolManager.Instance.GetPooledObject(ID);

        //    fx.transform.position = pos;
        //    fx.transform.forward = forward;
        //    fx.transform.parent = bulletSpawn.transform;

        //    fx.GetComponent<Pitcher>().ChangePitch();
        //}
    }
}

//MuzzleFlash flash = muzzleFlashPool.Get();
//flash.transform.position = bulletSpawn.transform.position;
//flash.transform.rotation = Quaternion.LookRotation(bulletSpawn.forward);
//flash.transform.parent = bulletSpawn.transform;
//flash.Deactivate();

//shake.start = true;


//MuzzleFlashServerRpc(bulletSpawn.transform.position, bulletSpawn.forward);




//GetComponent<AudioSource>().clip = PerkDatabase.Instance.GetSFXByID(1).SFX;
//GetComponent<AudioSource>().Play();



//BulletTraces trace = bulletTracePool.Get();
//trace.SetPoints(bulletSpawn.position + bulletSpawn.forward * 2, targetPoint);
//trace.transform.rotation = Quaternion.LookRotation(bulletSpawn.forward * -1);
//trace.Deactivate();

//BulletTraceServerRpc(bulletSpawn.position, targetPoint, bulletSpawn.forward * -1);

//if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Enemy"))
//{
//    if (hit.collider.gameObject.CompareTag("Player"))
//    {

//        TakeDamageServerRpc(
//                    hit.collider.gameObject.GetComponent<NetworkObject>().OwnerClientId,
//                    ownerStats.BulletDamage * DamageScale,
//                    ownerStats.BulletDamageType,
//                    ownerStats.SaltDamageModifier); // calculateBulletDamage(PlayerRegistry.GetStats(OwnerClientId),1)
//    }

//    else
//    {


//        if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var hitRef))
//        {
//            EnemytakeDamageServerRpc(
//                    hitRef,
//                    ownerStats.BulletDamage * DamageScale,
//                    ownerStats.BulletDamageType,
//                    ownerStats.SaltDamageModifier);

//            foreach (var perk in ownerStats.EnemyDeathPerks)
//            {
//                if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
//                    TriggerDeathPerkServerRpc(perk.ID, hit.point, hit.normal, enemyRef);
//            }
//        }

//        else if ((hit.collider.GetComponentInParent<NetworkObject>()))
//        {
//            Debug.LogError("SMASH");

//            EnemytakeDamageServerRpc(
//                   hitRef,
//                   ownerStats.BulletDamage * DamageScale,
//                   ownerStats.BulletDamageType,
//                   ownerStats.SaltDamageModifier);

//            foreach (var perk in ownerStats.EnemyDeathPerks)
//            {
//                if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
//                    TriggerDeathPerkServerRpc(perk.ID, hit.point, hit.normal, enemyRef);
//            }
//        }

//    }


//    foreach (var perk in ownerStats.OnHitPerks)
//    {
//        if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var hitRef))
//            TriggerPerkServerRpc(perk.ID, hit.point, hit.normal, hitRef);
//    }


//}

//else
//{
//    MuzzleFlash impact = impactFlashPool.Get();
//    impact.transform.position = hit.point;
//    impact.transform.rotation = Quaternion.LookRotation(hit.normal);
//    impact.Deactivate();

//    foreach (var perk in ownerStats.OnImpactPerks)
//    {

//        TriggerPerkServerRpc(perk.ID, hit.point, hit.normal);
//    }

//    ImpactFlashServerRpc(hit.point, hit.normal);

//}

