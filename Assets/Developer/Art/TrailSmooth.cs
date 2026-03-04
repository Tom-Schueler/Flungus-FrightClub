using UnityEngine;

public class TrailSmooth : MonoBehaviour
{
    public Transform target; // Assign NetObject's actual transform
    public float followSpeed = 15f;
    public float lifeTime = 2;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * followSpeed);

    }
}
