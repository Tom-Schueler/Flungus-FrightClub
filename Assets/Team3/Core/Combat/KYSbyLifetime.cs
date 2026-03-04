using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class KYSbyLifetime : MonoBehaviour
{
    public float Lifetime = 3f;

    public void Update()
    {
        Lifetime -= Time.deltaTime;
        if (Lifetime < 0)
        {
            if (gameObject.TryGetComponent<NetworkObject>(out NetworkObject networkObject))
            {
                networkObject.Despawn(true);
            }
            else 
            { 
                Destroy(gameObject); 
            }
        }
    }
}
