using Unity.Netcode;
using UnityEngine;

namespace Team3.MOBA
{
    public class Thommy : WeaponBase
    {

        [SerializeField] MixedProjectile mixedProjectile;
        [SerializeField] MixedServerProjectile mixedServerProjectile;
        [SerializeField] Animation weaponAnimation;

        [SerializeField] AnimationClip shoot;
        [SerializeField] AnimationClip reload;

        public override void Shoot(Vector3 hitPoint, bool hasHit, bool triggerPerks, RaycastHit hitTarget)
        {
            base.Shoot(hitPoint, hasHit, triggerPerks, hitTarget);

            weaponAnimation.Play(shoot.name);

            var dir = (hitPoint - bulletSpawn.position).normalized;

            var proj = Instantiate(mixedProjectile, bulletSpawn.position, Quaternion.LookRotation(dir));
            proj.Initialize();

            ShootServerRpc(bulletSpawn.position, dir,OwnerClientId);
           
        }

        public override void Reload()
        {
            weaponAnimation.Play(reload.name);
            base.Reload();

        }

        public override void SetBulletElement(Weapons.DamageType elementalType)
        {
            base.SetBulletElement(elementalType);
        }

        [ServerRpc]
        void ShootServerRpc(Vector3 pos, Vector3 dir, ulong owner)
        {
            var proj = Instantiate(mixedServerProjectile, pos, Quaternion.LookRotation(dir));
            proj.GetComponent<NetworkObject>().Spawn();
            proj.Initialize(owner,gunData.AttackDamageScaling);

            ShootClientRpc(pos, dir);
        }


        [ClientRpc]
        void ShootClientRpc(Vector3 pos, Vector3 dir)
        {
            if (IsOwner) return;
            var proj = Instantiate(mixedProjectile, pos, Quaternion.LookRotation(dir));
            proj.Initialize();
        }
    }
}