using Team3.Characters;
using Team3.Multiplayer;
using Unity.Netcode;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (MatchCycle.isDeathzoneAktive.Value)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent(out CharacterStats no))
                {
                    no.TakeDamage(500);
                }
            }
        }

        
    }
}
