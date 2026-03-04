using System.Collections.Generic;
using System.Xml;
using Team3.Characters;
using Team3.Combat;
using Team3.Weapons;
using Unity.Netcode;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float Force;
    public float ProjectileGravity;
    public float ProjectileLifetime;
    public float ProjectileSize;
    public int NumberOfBounces;
    public int NumberOfPierces;
    public bool destroyedOnImpact = true;
    public HashSet<GameObject> hitCharacters = new HashSet<GameObject>();
    public Rigidbody rb;
    public ulong ownerID;
    public List<SOProjectilePerk> BehaviourPerks = new List<SOProjectilePerk>();
    public float TriggerBehaviourPerkTimer = 0;
    public float maxSpeed = 500;
    public bool hitWall = false;
    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnSpawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(gameObject.transform.forward.normalized * (Force * 0.1f), ForceMode.Impulse);
          

}


private void FixedUpdate()
    {
        if (hitWall) { 
        Vector3 customGravity = ProjectileGravity * Physics.gravity;
        rb.AddForce(customGravity, ForceMode.Acceleration);

        foreach (var perk in BehaviourPerks)
        {
            Debug.Log(perk.name);
            perk.TriggerPerk(null, ownerID, this.gameObject.transform.position, this.gameObject.transform.forward);
        }

        if(rb.linearVelocity.magnitude >= maxSpeed) {
            Debug.Log(rb.linearVelocity.magnitude);
        rb.linearVelocity = rb.linearVelocity.normalized * 500;
        }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

    }

    private void Update()
    {
        Vector3 velocity = rb.linearVelocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            // Rotate the forward direction to match the velocity
            transform.rotation = Quaternion.LookRotation(velocity.normalized);
        }

    }

 


    private void OnTriggerEnter(Collider other)
    {
        
        bool otherPlayer = false;

        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<NetworkObject>().OwnerClientId != ownerID)
            {
                otherPlayer = true;
            }
        }

        if ((other.CompareTag("Enemy") || otherPlayer))
        {

            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            hitWall = true;

            hitCharacters.Add(other.gameObject);
            
            if(NumberOfPierces > 0)
            {
                NumberOfPierces--;
                return;
            }

            if(NumberOfBounces > 0)
            {
                Bounce();
                NumberOfBounces--;
            }
            else { 
                CheckForDestroy();
            }

            if(gameObject.TryGetComponent<ProjectileVisuals>(out ProjectileVisuals pvis)){
                pvis.SpawnFX();
            }

        }

        else if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {

        }

        else
        {

            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            hitWall = true;

            if (gameObject.TryGetComponent<ProjectileVisuals>(out ProjectileVisuals pvis))
            {
                pvis.SpawnFX();
            }

            if (NumberOfBounces > 0)
            {
                Bounce();
                NumberOfBounces--;
            }
            else
            {
                CheckForDestroy();
            }

        }
        
    }

    private void CheckForDestroy()
    {
        Destroy(gameObject);
    }
    private void Bounce()
    {
        Ray ray = new Ray(transform.position, GetComponent<Rigidbody>().linearVelocity.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f))
        {
            Vector3 inDirection = GetComponent<Rigidbody>().linearVelocity;
            Vector3 reflected = Vector3.Reflect(inDirection.normalized, hit.normal);

            GetComponent<Rigidbody>().linearVelocity = reflected * inDirection.magnitude;
            transform.forward = reflected;
           
        }
    }
}
