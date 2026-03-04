using System.Collections;
using System.Collections.Generic;
using Team3.Characters;
using UnityEngine;

namespace Team3.MOBA
{
    public class WeaponSway : MonoBehaviour
    {

        [Header("Sway Settings")]
        [SerializeField] private float rotationSteps = 4f;
        [SerializeField] private float maxRotationStep = 5f;
        Vector3 swayEulerRot;
        [SerializeField] CharacterMovement cmovement;
        Vector2 lookInput;
        [SerializeField]
        float smoothRot;

        private void Update()
        {
            lookInput = cmovement.LookInput;
            SwayRotation();
            CompositeRotation();
        }


        void LookInput()
        {

        }
        void SwayRotation()
        {
            Vector2 invertLook = lookInput * -rotationSteps;
            invertLook.x = Mathf.Clamp(invertLook.x,-maxRotationStep,maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y,-maxRotationStep,maxRotationStep);

            swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);

        }

        void CompositeRotation()
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot), Time.deltaTime * smoothRot);
        }

       
    }
}