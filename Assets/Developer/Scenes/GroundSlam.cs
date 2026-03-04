using System.Drawing;
using Team3.Characters;
using Team3.Weapons;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Unity.Netcode;

namespace Team3.Enemys
{
    public class GroundSlam : MonoBehaviour
    {
        [SerializeField]
        float damage;

        [SerializeField]
        float force;

        [SerializeField]
        DecalProjector projector;

        [SerializeField]
        float slamArea;

        [SerializeField]
        LayerMask AffectedLayers;

        [SerializeField]
        float LifeTime = 10f;



        float size = 0f;

        private void Start()
        {
            projector.size = new Vector3(0, 0, 1.5f);
            Invoke("Slam", 0.2f);
        }


        private void Update()
        {
           


            if(LifeTime < 2f)
            {
                size = Mathf.Clamp(size - Time.deltaTime * 20f, 0f, 10f);
                projector.size = new Vector3(size, size, 1.5f);
            }
            else
            {
                size = Mathf.Clamp(size + Time.deltaTime * 20f, 0f, 10f);
                projector.size = new Vector3(size, size, 1.5f);
            }
            if (LifeTime < 0)
            {
                Destroy(gameObject);
            }

            LifeTime -= Time.deltaTime;
        }


        public void Slam()
        {

            Collider[] hits = Physics.OverlapSphere(transform.position, slamArea, AffectedLayers);

            foreach (var hit in hits)
            {
                Rigidbody rb = hit.attachedRigidbody;
                if (rb != null)
                {

                    if (hit.TryGetComponent<NetworkCharacter>(out var move))
                    {
                        move.ApplyExplosionForceClientRpc((hit.transform.position-new Vector3(0,50,0)), force, hit.GetComponent<NetworkObject>().OwnerClientId);
                    }

                    if (hit.TryGetComponent<CharacterStats>(out var stats))
                    {
                        stats.TakeDamage(damage, DamageType.Ice);
                        Debug.LogError("SO EINE ICE-SCHELLE JUNGE" + damage);
                    }
                }
            }
        }
    }
}
