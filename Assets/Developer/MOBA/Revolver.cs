using Team3.Enemys.Common;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace Team3.MOBA
{
    public class Revolver : WeaponBase
    {
        [SerializeField] SOVFX impactFX;

        [SerializeField] Animation weaponAnimation;

        [SerializeField] AnimationClip shoot;
        [SerializeField] AnimationClip reload;


        public override void Shoot(Vector3 hitPoint, bool hasHit, bool triggerPerks, RaycastHit hitTarget)
        {
            //base.Shoot(hitPoint, hasHit, triggerPerks, hitTarget);

            weaponAnimation.Play(shoot.name);
            if (hitTarget.collider != null)
            {
                ownerStats = PlayerRegistry.GetStats(OwnerClientId);
                if (hitTarget.collider.gameObject.CompareTag("Player") || hitTarget.collider.gameObject.CompareTag("Enemy"))
                {
                    if (hitTarget.collider.gameObject.CompareTag("Player"))
                    {

                        TakeDamageServerRpc(
                                    hitTarget.collider.gameObject.GetComponent<NetworkObject>().OwnerClientId,
                                    ownerStats.BulletDamage * gunData.AttackDamageScaling,
                                    ownerStats.BulletDamageType,
                                    ownerStats.SaltDamageModifier, 
                                    (int)OwnerClientId,
                                    hitPoint); 
                    }

                    else
                    {


                        if (hitTarget.collider.gameObject.TryGetComponent<NetworkObject>(out var hitRef))
                        {
                            EnemytakeDamageServerRpc(
                                    hitRef,
                                    ownerStats.BulletDamage * gunData.AttackDamageScaling,
                                    ownerStats.BulletDamageType,
                                    ownerStats.SaltDamageModifier, (int)OwnerClientId);

                            foreach (var perk in ownerStats.EnemyDeathPerks)
                            {
                                if (hitTarget.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
                                    TriggerDeathPerkServerRpc(perk.ID, hitTarget.point, hitTarget.normal, enemyRef);
                            }
                        }

                        else if ((hitTarget.collider.GetComponentInParent<NetworkObject>()))
                        {


                            EnemytakeDamageServerRpc(
                                   hitRef,
                                   ownerStats.BulletDamage * gunData.AttackDamageScaling,
                                   ownerStats.BulletDamageType,
                                   ownerStats.SaltDamageModifier, (int)OwnerClientId);

                            foreach (var perk in ownerStats.EnemyDeathPerks)
                            {
                                if (hitTarget.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
                                    TriggerDeathPerkServerRpc(perk.ID, hitTarget.point, hitTarget.normal, enemyRef);
                            }
                        }

                    }


                    foreach (var perk in ownerStats.OnHitPerks)
                    {
                        if (hitTarget.collider.gameObject.TryGetComponent<NetworkObject>(out var hitRef))
                            TriggerPerkServerRpc(perk.ID, hitTarget.point, hitTarget.normal,OwnerClientId, hitRef);
                    }

                  
                    ImpactFlashServerRpc(hitTarget.point, hitTarget.normal, impactFX.ID);
                }

                else
                {
                    int index = 0;
                    foreach (var perk in ownerStats.OnImpactPerks)
                    {
                        if (Time.time - ownerStats.OnImpactPerksTrackCooldown[index] > perk.CoolDown)
                        {
                            ownerStats.OnImpactPerksTrackCooldown[index] = Time.time;


                            TriggerPerkServerRpc(perk.ID, hitTarget.point, hitTarget.normal,OwnerClientId);

           
                        }
                        index++;
                    }

                    
                    ImpactFlashServerRpc(hitTarget.point, hitTarget.normal, impactFX.ID);

                }


            }
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



        [ClientRpc]
        void ShootClientRpc(Vector3 pos, Vector3 dir)
        {
        }


        [ClientRpc]
        void TriggerPerkClientRpc(int perkid, Vector3 pos, Vector3 dir, ulong owner)
        {
            PerkDatabase.Instance.GetPerkByID(perkid).ClientTrigger(pos, dir, owner, -1);
        }


        [ServerRpc]
        public void TakeDamageServerRpc(ulong id, float damage, DamageType type, float saltmodifier, int ownerID, Vector3 hitPoint)
        {
            PlayerRegistry.GetStats(id).TakeDamage(damage, type, 1, saltmodifier,ownerID, hitPoint);

            bool hitsound;
            if (PlayerRegistry.GetStats(id).currentHealth.Value <= 0)
            {
                hitsound = true;
            }
            else
            {
                hitsound = false;
            }
                PlayHitmarkerClientRpc((ulong)ownerID, hitsound);


        }

        [ServerRpc]
        public void TriggerPerkServerRpc(int id, Vector3 pos, Vector3 dir,ulong ownerID, NetworkObjectReference hitRef = default)
        {
            PerkDatabase.Instance.GetPerkByID(id).ServerTrigger(pos, dir, OwnerClientId, -1);
            TriggerPerkClientRpc(id, pos, dir, (ulong)ownerID);
            
            //PerkDatabase.Instance.GetPerkByID(id).TriggerPerk(null, 420, pos, dir, hitRef);

        }

        [ServerRpc]
        public void TriggerDeathPerkServerRpc(int id, Vector3 pos, Vector3 dir, NetworkObjectReference hitRef = default)
        {
            PerkDatabase.Instance.GetPerkByID(id).TriggerDeathPerk(null, 420, pos, dir, hitRef);

        }

        [ServerRpc]
        public void EnemytakeDamageServerRpc(NetworkObjectReference hitRef, float damage, DamageType type, float saltmodifier,int ownerID)
        {
            if (hitRef.TryGet(out NetworkObject hitObject))
            {
                Debug.LogError("hello");
                // Now try to get the component from the resolved object
                if (hitObject.TryGetComponent<EnemyStats>(out var stats))
                {
                }
                else
                {

                    stats = hitObject.gameObject.GetComponentInChildren<EnemyStats>();
                }

                stats.TakeDamage(damage * saltmodifier, type, 1, saltmodifier,ownerID);




                int hitsound;
                //if (stats.GetComponent<EnemyStats>().currentHealth.Value <= 0)
                //{
                    //hitsound = hitMarkerKillSound.ID;
                //}
                //else
                //{
                    hitsound = hitMarkerSound.ID;
                //}
                PlayHitmarkerClientRpc((ulong)ownerID, false);
            }
        }

        [ServerRpc]
        public void ImpactFlashServerRpc(Vector3 position, Vector3 rotation,int id)
        {
            ImpactFlashClientRpc(position, rotation, id);
        }


        [ClientRpc]
        public void ImpactFlashClientRpc(Vector3 position, Vector3 rotation,int fxID)
        {
            //if (IsOwner) return;

            GameObject fx = ObjectPoolManager.Instance.GetPooledObject(fxID);
            fx.transform.position = position;
            fx.transform.forward = rotation;
            fx.GetComponent<Pitcher>().ChangePitch();
        }

        [ClientRpc]
        public void PlayHitmarkerClientRpc(ulong id, bool killingBLow)
        {

           
            if (id == NetworkManager.Singleton.LocalClientId)
            {
                PlayerRegistry.GetStats(id).PlayHitmarker(killingBLow);
              
            }
        }


    }
}