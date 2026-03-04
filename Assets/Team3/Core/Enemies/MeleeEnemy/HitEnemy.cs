using Team3.Characters;
using UnityEngine;

namespace Team3.Enemys.MeeleEnemy
{
    public class HitEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject hitFX;
        [SerializeField] private BasicMeleeAttackData basicMeleeAttackData;
        
        public void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(hitFX, transform.position, Quaternion.identity);
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(basicMeleeAttackData.Damage, basicMeleeAttackData.DamageType);

            }
        }
    }
}
