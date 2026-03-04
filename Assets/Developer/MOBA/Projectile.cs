using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Team3.MOBA
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float Speed = 10;
        [SerializeField] Vector3 LastPosition;
        [SerializeField] LayerMask layerMask = 0;
        [SerializeField] float maxLifetime = 2;

        [SerializeField] SOVFX impactFX;
        [SerializeField] float sphereRadius = 0.2f;
        [SerializeField] private Vector3 gravity = new Vector3(0, -9.81f, 0);
        private Vector3 velocity;


        public void Initialize()
        {
            LastPosition = transform.position;
            velocity = transform.forward * Speed;
        }
        public void FixedUpdate()
        {
            velocity += gravity * Time.fixedDeltaTime;
            transform.position = transform.position + velocity * Time.fixedDeltaTime;

            //transform.position += transform.forward * Speed * Time.fixedDeltaTime;

            if (Physics.SphereCast(LastPosition, sphereRadius, (transform.position - LastPosition).normalized, out RaycastHit hit, (transform.position - LastPosition).magnitude, layerMask))
            {

            //    if (Physics.Raycast(LastPosition, transform.forward, out hit, (transform.position-LastPosition).magnitude, layerMask))

            //{

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