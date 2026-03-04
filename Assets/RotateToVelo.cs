using UnityEngine;

public class RotateToVelo : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    // Update is called once per frame
    void Update()
    {
        transform.forward = rb.linearVelocity;
    }
}
