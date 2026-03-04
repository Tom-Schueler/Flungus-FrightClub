using UnityEngine;

namespace Team3.MOBA
{
    
    public class Recoil : MonoBehaviour
    {
        public Vector3 currentRotation;
        public Vector3 targetRotation;
        public Vector3 recoil;

        public float snappiness;
        public float returnSpeed;

        public void Update()
        {
            targetRotation = Vector3.Lerp(targetRotation,Vector3.zero, returnSpeed*Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }

        public void RecoilFire()
        {
            targetRotation += new Vector3(recoil.x, Random.Range(-recoil.y, recoil.y), Random.Range(-recoil.z, recoil.z));
        }
    }
}