using UnityEngine;
using Team3.Weapons;
using System;
using System.Collections;
using Unity.Netcode;
using Team3.Combat;

namespace Team3.Characters
{
    [Serializable]
    public class CharacterStats : NetworkBehaviour
    {

        //####### DEBUG REMOVE THIS #######
        public SOCombatCards debugPerk;
        public SOCombatCards debugPerk2;
        public bool fliflop = true;


        //####### Base Stats #######
        public float originalSpeed;
        public float currentSpeed;

        public float originalHealth;
        public float health;
        public NetworkVariable<float> currentHealth = new NetworkVariable<float>(1000f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);


        //####### Debuff Coroutines / Timers #######
        public Coroutine slowCoroutine;
        public Coroutine rootCoroutine;
        public Coroutine fireCoroutine;
        public Coroutine iceCoroutine;
        public Coroutine waterCoroutine;

        public int iceStacks;
        public int fireStacks;
        public int waterStacks;

        public float damageDealReduction = 1f;
        public float debuffDuration = 5;


        private void Awake()
        {
            currentSpeed = originalSpeed;
            health = originalHealth;
        }

        //####### SLOW #######
        public void ApplySlow(float amount, float duration)
        {
            if (slowCoroutine != null)
                StopCoroutine(slowCoroutine);

            slowCoroutine = StartCoroutine(SlowRoutine(amount, duration));
        }

        private IEnumerator SlowRoutine(float amount, float duration)
        {
            currentSpeed = originalSpeed * (1f - amount);
            yield return new WaitForSeconds(duration);
            currentSpeed = originalSpeed;
        }

        //####### ICE #######
        public void ApplyIce(float duration, int stackSize)
        {
            if (iceCoroutine != null)
                StopCoroutine(iceCoroutine);

            iceCoroutine = StartCoroutine(IceRoutine(duration, stackSize));
        }

        private IEnumerator IceRoutine(float duration, int stackSize)
        {
            iceStacks += stackSize;
            currentSpeed = Mathf.Clamp(originalSpeed * (1f - iceStacks), 0, originalSpeed);
            yield return new WaitForSeconds(duration);
            iceStacks = 0;
            currentSpeed = originalSpeed;
        }

        //####### FIRE #######
        public void ApplyFire(float duration, int stackSize)
        {
            if (fireCoroutine != null)
                StopCoroutine(fireCoroutine);

            fireCoroutine = StartCoroutine(FireRoutine(duration, stackSize));
        }

        private IEnumerator FireRoutine(float duration, int stackSize)
        {
            fireStacks += stackSize;
            yield return new WaitForSeconds(duration);
            fireStacks = 0;
        }

        //####### WATER #######
        public void ApplyWater(float duration, int stackSize)
        {
            if (waterCoroutine != null)
                StopCoroutine(waterCoroutine);

            waterCoroutine = StartCoroutine(WaterRoutine(duration, stackSize));
        }

        private IEnumerator WaterRoutine(float duration, int stackSize)
        {
            waterStacks += stackSize;
            damageDealReduction = Mathf.Clamp(1 - waterStacks, 0.5f, 1);
            yield return new WaitForSeconds(duration);
            waterStacks = 0;
            damageDealReduction = 1f;
        }

        //####### ROOT #######
        public void ApplyRoot(float duration)
        {
            if (rootCoroutine != null)
                StopCoroutine(rootCoroutine);

            rootCoroutine = StartCoroutine(RootRoutine(duration));

        }

        private IEnumerator RootRoutine(float duration)
        {
            currentSpeed = 0f;
            yield return new WaitForSeconds(duration);
            currentSpeed = originalSpeed;
        }

    

        public virtual void OnDeath() { }

        //####### Take Damage #######
        public virtual void TakeDamage(float damage, DamageType affix = DamageType.None, int stackSize = 1, float saltModifier = 1, int owner = -1, Vector3? hitPoint = null)
        {
            switch (affix)
            {
                case DamageType.Fire:
                    ApplyFire(debuffDuration, stackSize);
                    break;
                case DamageType.Ice:
                    ApplyIce(debuffDuration, stackSize);
                    break;
                case DamageType.Water:
                    ApplyWater(debuffDuration, stackSize);
                    break;
                default:
                    break;
            }

            if (currentHealth.Value <= 0f)
            {
                OnDeath();
            }
        }
    }
}