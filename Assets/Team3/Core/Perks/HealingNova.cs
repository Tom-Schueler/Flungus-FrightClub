using Team3.Characters;
using Unity.Netcode;
using UnityEngine;

public class HealingNova : MonoBehaviour
{


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerSlippery(other);
        }
    }

  
    public void SetPlayerSlippery(Collider player)
    {
        if(player.gameObject.TryGetComponent<PlayerStats>(out PlayerStats stats)) { 
        stats.FrozenGround();
        }
    }

}
