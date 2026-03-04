using System;
using System.Collections;
using Team3.Characters;
using Unity.Netcode;
using UnityEngine;

public class Implosion : NetworkBehaviour
{
    public Vector3 position;

    public float ExplosionRadius;

    public float ExplosionForce;

  
    public LayerMask AffectedLayers;

    public float Damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
        StartCoroutine(MyImplosion());
    }

   
    

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator MyImplosion()
    {

        Collider[] hits = Physics.OverlapSphere(position, ExplosionRadius, AffectedLayers);


        foreach (var hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;
            if (rb != null)
            {
                if (hit.TryGetComponent<NetworkCharacter>(out var move))
                {
                    move.ApplyImplosionForceClientRpc(position, ExplosionForce, hit.GetComponent<NetworkObject>().OwnerClientId);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<NetworkObject>().Despawn(true);
    }

        //RequestClientImplosionServerRpc(position);

        //yield return new WaitForSeconds(0.5f);

        //RequestClientExplositionServerRpc(position); 
        


        //hits = Physics.OverlapSphere(position, ExplosionRadius, AffectedLayers);


        //foreach (var hit in hits)
        //{
        //    Rigidbody rb = hit.attachedRigidbody;
        //    if (rb != null)
        //    {

        //        if (hit.TryGetComponent<NetworkCharacter>(out var move))
        //        {
        //            move.ApplyExplosionForceClientRpc(position, ExplosionForce, hit.GetComponent<NetworkObject>().OwnerClientId);
        //        }
        //    }
        //}
    

    [ServerRpc]
    public void RequestClientImplosionServerRpc(Vector3 position)
    {
        ImplosionClientRpc(position);
    }

    [ClientRpc]
    public void ImplosionClientRpc(Vector3 position)
    {
        
        Instantiate(PerkDatabase.Instance.GetVFXByID(20).VFX, position, Quaternion.identity);
    }
    
    
    [ClientRpc]
    public void ExplosionClientRpc(Vector3 position)
    {
        Instantiate(PerkDatabase.Instance.GetVFXByID(21).VFX, position, Quaternion.identity);
    }

    [ServerRpc]
    public void RequestClientExplositionServerRpc(Vector3 position)
    {
        ExplosionClientRpc(position);
    }
}
