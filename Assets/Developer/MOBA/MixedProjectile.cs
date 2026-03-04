using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Team3.MOBA
{
    public class MixedProjectile : MonoBehaviour
    {
        [SerializeField] float Speed = 10;
        [SerializeField] Vector3 LastPosition;
        [SerializeField] LayerMask layerMask = 0;
        [SerializeField] float maxLifetime = 2;

        [SerializeField] SOVFX impactFX;


        public void Initialize()
        {
            LastPosition = transform.position;
        }
        public void FixedUpdate()
        {

            transform.position += transform.forward * Speed * Time.fixedDeltaTime;

            RaycastHit hit;

            if (Physics.Raycast(LastPosition, transform.forward, out hit, (transform.position-LastPosition).magnitude, layerMask))

            {

                GameObject fx = ObjectPoolManager.Instance.GetPooledObject(impactFX.ID);
                fx.transform.position = hit.point;
                fx.transform.forward = hit.normal;
                fx.GetComponent<Pitcher>().ChangePitch();

                Destroy(gameObject);
            }
            LastPosition = transform.position;
        }

        public void Update()
        {
            if(maxLifetime <0)
            {
                Destroy(gameObject);
            }
            maxLifetime -= Time.deltaTime;
        }
    }
}