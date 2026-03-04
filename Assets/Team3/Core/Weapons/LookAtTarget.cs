using UnityEngine;

namespace Team3.Weapons
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        private Vector3 hitPos;
        

        private void FixedUpdate()
        {
            RaycastFromCenter();
          
        }

        private void LateUpdate()
        {
            LookAtTargetPoint();
        }

        void RaycastFromCenter()
        {
            
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            Ray ray = cam.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                hitPos = hit.point;
            }
            else
            {
                Vector3 point = ray.origin + ray.direction * 100f;
                hitPos = point;
            }

            
        }


        void LookAtTargetPoint()
        {

            transform.LookAt(hitPos);

            Debug.Log(transform.forward);
        }
    }
}