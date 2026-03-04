using System;
using Team3.Characters;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

namespace Team3.Enemys.Common
{
    public class EnemyStats : CharacterStats
    {
        public static Action<int> OnEnemyDeath;
        public static Action<int> OnDeathToSpawner;

        public DamageTypeValue weakness;
        //public float debuffDuration;
        public GameObject textPop;
        public float textTrigger;
        public GameObject deathFX;
        public bool isDead = false;
        public int killerID = -1;
        public int SpawnerID = -1;

        public void Start()
        {
            
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            currentSpeed = originalSpeed;
            currentHealth.Value = originalHealth;
        }

        //####### Update #######
        public void Update()
        {
            if(isDead) return;
            if (fireStacks > 0)
            {
                currentHealth.Value -= fireStacks * Time.deltaTime;
            }

            textTrigger += Time.deltaTime;

            if (textTrigger > 1)
            {
                textTrigger = 0f;
               // if (fireStacks > 0)
               // {
               //     DisplayDamage dmgPop = Instantiate(textPop, transform.position, Quaternion.identity).GetComponent<DisplayDamage>();
                //    dmgPop.SetDamageText((float)fireStacks, DamageType.Fire);
               // }
            }


            if (currentHealth.Value < 0)
            {
                isDead = true;
                var deathfx = Instantiate(deathFX, transform.position, Quaternion.identity);
                deathfx.GetComponent<NetworkObject>().Spawn();
                Die();
            }
        }

        public void Die()
        {
            GetComponent<EnemyPerkHandler>().OnDeathPerks();

            OnDeathToSpawner?.Invoke(SpawnerID);
            if (killerID != -1)
            {
                InvokeDeathClientRpc(killerID, transform.position);
            }

            if (IsServer && NetworkObject.IsSpawned)
            {
                NetworkObject.Despawn();

            }
        }

        [ClientRpc]
        private void InvokeDeathClientRpc(int killerID, Vector3 pos)
        {
            if ((ulong)killerID == NetworkManager.Singleton.LocalClientId)
            {
                Debug.LogError("BOOM");
                Instantiate(PerkDatabase.Instance.GetVFXByID(25).VFX, pos, Quaternion.identity);
                Debug.LogError("BOOM 2  " );
                OnEnemyDeath?.Invoke(killerID);
            }
        }


        public override void TakeDamage(float damage, DamageType affix = DamageType.None, int stackSize = 1, float saltModifier = 1, int owner = -1, Vector3? pos = null)
        {
            killerID = owner;
            currentHealth.Value -= damage;
            base.TakeDamage(damage, affix, stackSize, saltModifier);
        }
    }
}