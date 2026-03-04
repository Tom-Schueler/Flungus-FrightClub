using UnityEngine;

public class PlayerTriggerEnter : MonoBehaviour {

    [SerializeField]
    EnemyTurret enemyTurret;

    private void OnTriggerEnter(Collider other)
    {
        enemyTurret.TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        enemyTurret.TriggerExit(other);
    }

}
