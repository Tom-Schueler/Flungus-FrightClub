using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public AnimationCurve zoom;
    public float duration = 1f;
    public CinemachineBasicMultiChannelPerlin noise;
    public CinemachineCamera cam;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = Mathf.Clamp(curve.Evaluate(elapsedTime),0,5);
            float zoomStrength = 65 - Mathf.Clamp(curve.Evaluate(elapsedTime), 0, 5);
            noise.AmplitudeGain = strength;
            cam.Lens.FieldOfView = zoomStrength;
            yield return null;
        }
    }
}
