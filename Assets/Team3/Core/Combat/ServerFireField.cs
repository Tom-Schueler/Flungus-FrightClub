using Team3.Characters;
using Team3.Enemys.Common;
using Unity.Netcode;
using UnityEngine;

public class ServerFireField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent<EnemyStats>(out var stats))
            {
                stats.TakeDamage(0, Team3.Weapons.DamageType.Fire, 10);
            }
        }
    }
}