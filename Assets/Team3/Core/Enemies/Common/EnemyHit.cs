using UnityEngine;
using System.Collections.Generic;
using Team3.Characters;
using Team3.Weapons;
using Unity.Netcode;

namespace Team3.Enemys.Common
{
    public class EnemyHit : NetworkBehaviour
    {
        [SerializeField] private GameObject explosion;
        [SerializeField] private EnemyStats stats;
        [SerializeField] private GameObject dmgText;

        public Dictionary<float, int> namess = new Dictionary<float, int>();
        
        public void TakeDamage(float damage, DamageType type, DamageType affix, int stackSize)
        {
            if (type == stats.weakness.type)
            {
                damage *= 1 + stats.weakness.value / 100;
            }

            stats.TakeDamage(damage);

            switch (affix)
            {
                case DamageType.Fire:
                    stats.ApplyFire(stats.debuffDuration, stackSize);
                    break;
                case DamageType.Ice:
                    stats.ApplyIce(stats.debuffDuration, stackSize);
                    break;
                case DamageType.Water:
                    stats.ApplyWater(stats.debuffDuration, stackSize);
                    break;
                default:
                    break;
            }

            DisplayDamageNumbersClientRpc(damage, affix);
        }

        [ClientRpc]
        public void DisplayDamageNumbersClientRpc(float damage,DamageType affix)
        {
            DisplayDamage dmgPop = Instantiate(dmgText, transform.position, Quaternion.identity).GetComponent<DisplayDamage>();
            dmgPop.SetDamageText((int)damage, affix);
        }
    }
}