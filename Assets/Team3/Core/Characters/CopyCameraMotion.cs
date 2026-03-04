using UnityEngine;

public class CopyCameraMotion : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    private void Update()
    {
        // Directly follow the camera's position and rotation without smoothing
        transform.position = m_Camera.transform.position;
        transform.rotation = m_Camera.transform.rotation;
    }

}
