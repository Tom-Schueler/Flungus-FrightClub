using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MuzzleFlash : MonoBehaviour 
{
    [SerializeField]
    private float journeyTime = 0.2f;


    private IObjectPool<MuzzleFlash> muzzleFlashPool;
    public IObjectPool<MuzzleFlash> MuzzleFlashPool {  set => muzzleFlashPool = value; }


    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(journeyTime));
    }


    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        muzzleFlashPool.Release(this);
    }
}
