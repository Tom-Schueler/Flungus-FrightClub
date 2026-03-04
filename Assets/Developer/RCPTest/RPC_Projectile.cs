using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleProjectile : NetworkBehaviour
{
    [SerializeField] private float speed = 20f;
    private Rigidbody rb;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
           
        }
    }

    public void Setup()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
    }
}