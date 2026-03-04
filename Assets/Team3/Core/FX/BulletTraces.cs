using NUnit.Framework.Internal;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletTraces : MonoBehaviour 
{
    [SerializeField]
    private float journeyTime = 1f;
    [SerializeField]
    private LineRenderer lineRenderer;

    private Vector3 start, end;

    private float startTime;

    private IObjectPool<BulletTraces> bulletTracePool;
    public IObjectPool<BulletTraces> BulletTracePool {  set =>  bulletTracePool = value; }

    public void SetPoints(Vector3 start, Vector3 end)
    {
        journeyTime = 0.5f* ((start - end).magnitude / 100f);
        startTime = Time.time;
        this.start = start;
        this.end = end;
    }

    public void Update()
    {
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Lerp(start, end, fracComplete);
    }


    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(journeyTime));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletTracePool.Release(this);
    }
}
