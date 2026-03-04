using Team3.Multiplayer;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;

public class ProjectileShooter : NetworkBehaviour
{
    [SerializeField] private SimpleProjectile projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField]
    private GameObject ghost;

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ghostyServerRpc();
        }
    }

    [ServerRpc]
    public void ghostyServerRpc()
    {
        GameObject instant = Instantiate(ghost, transform.position, Quaternion.identity);
        NetworkObject no = instant.GetComponent<NetworkObject>();
        no.Spawn();
    }
}