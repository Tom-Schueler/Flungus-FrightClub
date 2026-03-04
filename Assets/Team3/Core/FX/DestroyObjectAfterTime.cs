using Unity.Netcode;
using UnityEngine;

public class DestroyObjectAfterTime : NetworkBehaviour
{
    public float lifeTime;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            if (IsServer && NetworkObject.IsSpawned)
            {
                NetworkObject.Despawn();

            }
            else
            {
                Destroy(gameObject);

            }
        }
    }

}
