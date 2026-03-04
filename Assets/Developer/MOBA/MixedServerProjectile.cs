using Team3.Characters;
using Team3.Enemys.Common;
using Unity.Netcode;
using UnityEngine;

namespace Team3.MOBA
{
    public class MixedServerProjectile : NetworkBehaviour
    {
        [SerializeField] float Speed = 10;
        [SerializeField] Vector3 LastPosition;
        [SerializeField] LayerMask layerMask = 0;
        [SerializeField] float maxLifetime = 2;
        [SerializeField] ulong ownerID;
        PlayerStats ownerStats;
        float damageScale;



        public void Initialize(ulong onwerID, float damageScale)
        {
            this.ownerID = onwerID;
            LastPosition = transform.position;
            ownerStats = PlayerRegistry.GetStats(ownerID);
            this.damageScale = damageScale;
        }
        public void FixedUpdate()
        {
            ownerStats = PlayerRegistry.GetStats(ownerID);
            transform.position += transform.forward * Speed * Time.fixedDeltaTime;

            RaycastHit hit;

            if (Physics.Raycast(LastPosition, transform.forward, out hit, (transform.position - LastPosition).magnitude, layerMask))

            {
                //Debug.LogError("Owner Stats " + ownerStats.BulletDamage + "  owner Damage Type " + ownerStats.BulletDamageType + "  DamageScale " + damageScale);
                //Debug.LogError(ownerStats.BulletDamage * damageScale);

                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Enemy"))
                {


                    int index = 0;

                    foreach (var perk in ownerStats.OnHitPerks)
                    {
                        if (Time.time - ownerStats.OnHitPerksTrackCooldown[index] > perk.CoolDown)
                        {
                            TriggerPerkClientRpc(perk.ID, hit.point, hit.normal, OwnerClientId);

                            if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
                            {
                                perk.ServerTrigger(hit.point, hit.normal, OwnerClientId, -1, enemyRef);
                            }

                            ownerStats.OnHitPerksTrackCooldown[index] = Time.time;
                        }
                        index++;
                    }


                    if (hit.collider.CompareTag("Player"))
                    {
                        hit.collider.gameObject.GetComponent<PlayerStats>().TakeDamage(ownerStats.BulletDamage * damageScale, ownerStats.BulletDamageType, 1, 1, (int)ownerID, hit.point);

                        bool killingBlow = hit.collider.gameObject.GetComponent<CharacterStats>().currentHealth.Value <= 0;

                        ShowHitmarkerClientRpc(ownerID, killingBlow);

                    }


                    else if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<EnemyStats>().TakeDamage(ownerStats.BulletDamage * damageScale, ownerStats.BulletDamageType, 1, ownerStats.SaltDamageModifier, (int)ownerID);

                        ShowHitmarkerClientRpc(ownerID, false);

                        foreach (var perk in ownerStats.EnemyDeathPerks)
                        {
                            if (hit.collider.gameObject.TryGetComponent<NetworkObject>(out var enemyRef))
                                PerkDatabase.Instance.GetPerkByID(perk.ID).TriggerDeathPerk(null, 420, hit.point, hit.normal, enemyRef);
                        }

                    }

                    if (IsServer) gameObject.GetComponent<NetworkObject>().Despawn(true);
                }


                else
                {
                    int index = 0;
                    foreach (var perk in ownerStats.OnImpactPerks)
                    {
                        if (Time.time - ownerStats.OnImpactPerksTrackCooldown[index] > perk.CoolDown)
                        {

                            TriggerPerkClientRpc(perk.ID, hit.point, hit.normal, OwnerClientId);


                            perk.ServerTrigger(hit.point, hit.normal, OwnerClientId, -1);


                            ownerStats.OnImpactPerksTrackCooldown[index] = Time.time;
                        }
                        index++;
                    }

                    if (IsServer) gameObject.GetComponent<NetworkObject>().Despawn(true);
                }

            }

            LastPosition = transform.position;
        }

        public void Update()
        {
            if (maxLifetime < 0)
            {
                if (IsServer) gameObject.GetComponent<NetworkObject>().Despawn(true);
            }
            maxLifetime -= Time.deltaTime;
        }


        [ClientRpc]
        void TriggerPerkClientRpc(int perkid, Vector3 pos, Vector3 dir, ulong owner)
        {
            PerkDatabase.Instance.GetPerkByID(perkid).ClientTrigger(pos, dir, owner, -1);
        }

        [ClientRpc]
        void ShowHitmarkerClientRpc(ulong owner, bool killingBlow)
        {
            if (owner == NetworkManager.Singleton.LocalClientId)
            {
                PlayerRegistry.GetStats(owner).PlayHitmarker(killingBlow);
            }
        }
    }
}