using Team3.Characters;
using UnityEngine;

public class HealingField : MonoBehaviour
{
    [SerializeField]
    private float healingOverTime = 10;


    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerStats>(out PlayerStats stats))
            {
                stats.TakeDamage(-healingOverTime * Time.fixedDeltaTime);
            }
            
        }
    }

}
