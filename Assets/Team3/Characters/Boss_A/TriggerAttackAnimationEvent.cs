using Team3.Enemys;
using UnityEngine;

public class TriggerAttackAnimationEvent : MonoBehaviour
{
    [SerializeField]
    BigBoyAttack BigBoyAttack;

    public void TriggerAttack()
    {
        BigBoyAttack.SpawnImpact();
    }
}
