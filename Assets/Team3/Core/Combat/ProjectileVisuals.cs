using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileVisuals : MonoBehaviour
{
    public GameObject ProjectileMesh;
    public GameObject ProjectileTrailFXAsset;
    public GameObject ProjectileImpactFXAsset;
    public ParticleSystem ps;
    public HashSet<GameObject> hitCharacters = new HashSet<GameObject>();

    public void SetupVisuals()
    {
        ProjectileTrailFXAsset = Instantiate(ProjectileTrailFXAsset, transform);
        ProjectileTrailFXAsset.transform.forward = transform.forward;

        ProjectileMesh = Instantiate(ProjectileMesh, transform);
        ProjectileMesh.transform.forward = transform.forward;

        ps = ProjectileImpactFXAsset.GetComponent<ParticleSystem>();
    }

    public void SpawnFX()
    {
        Instantiate(ProjectileImpactFXAsset, transform.position, Quaternion.identity);
    }


}
