using Team3.Enemys;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            //collision.gameObject.GetComponent<EnemyHit>().takeDamage();
        }
    }
}
