using Team3.Characters;
using UnityEngine;

namespace Team3.Combat
{
    public class SimpleProjectile : MonoBehaviour
    {
        [SerializeField]
        float damage;

        [SerializeField]
        float speed;

        [SerializeField]
        float lifeTime;
        float currentLife = 0;

        [SerializeField]
        GameObject impactVFX;

        [SerializeField]
        Rigidbody rb;

        private void Start()
        {
            rb.AddForce(transform.forward * speed,ForceMode.Impulse);
        }

        private void Update()
        {
            if(lifeTime < currentLife)
            {
                Instantiate(impactVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            else
            {
                currentLife += Time.deltaTime;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerStats>(out var stats))
            {
                stats.TakeDamage(damage);
            }
        }
    }
}
