using Team3.Characters;
using Unity.Netcode;
using UnityEngine;

public class FireField : MonoBehaviour
{


    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerOnFire(other);
        }
    }

  
    public void SetPlayerOnFire(Collider player)
    {
        if(player.gameObject.TryGetComponent<PlayerStats>(out PlayerStats stats)) { 
        stats.FireGround();
        }
    }

}
