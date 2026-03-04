using Team3.Characters;
using UnityEngine;

namespace Team3.Perks { 
public class IceField : MonoBehaviour
{

        private void Start()
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
        }

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
}
