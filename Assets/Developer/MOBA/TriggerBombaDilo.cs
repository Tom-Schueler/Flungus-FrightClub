using Team3.Enemys;
using UnityEngine;

public class TriggerBombaDilo : MonoBehaviour
{
    [SerializeField] BombaJump BombaJump;


    public void TriggerLeap()
    {
        BombaJump.Leap();
    }

    public void TriggerExplosion()
    {
        BombaJump.SpawnImpact();
    }
}
