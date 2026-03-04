using Team3.CustomMath;
using UnityEngine;

namespace Team3.Movement
{
    public static class FirstPersonMovement
    {
        public static void CalculateMoveVelocity(Rigidbody body, ref Vector3 currentMoveVelocity, Vector3 moveInput, float speed, bool isOnFloor, RaycastHit hitInfo)
        {
            var yawRotation = Quaternion.Euler(0f, body.transform.rotation.eulerAngles.y, 0f);
            Vector3 moveDir = yawRotation * moveInput;

            if (isOnFloor)
            {
                moveDir = Vector3.ProjectOnPlane(moveDir, hitInfo.normal).normalized;
            }

            currentMoveVelocity = speed * moveDir;
        }

        public static void CalculateMoveVelocity(Rigidbody body, ref Vector3 currentMoveVelocity, Vector3 moveInput, float speed, bool isOnFloor, RaycastHit hitInfo, float slopeThreshold, float maxSlopeAngle)
        {
            var yawRotation = Quaternion.Euler(0f, body.transform.rotation.eulerAngles.y, 0f);
            Vector3 moveDir = yawRotation * moveInput;

            if (isOnFloor)
            {
                moveDir = Vector3.ProjectOnPlane(moveDir, hitInfo.normal).normalized;
                float angle = TDVector.GetAngleToGroundPlane(moveDir, Vector3.up);

                if (angle > slopeThreshold)
                {
                    float percent = 1 - ((angle - slopeThreshold) / (maxSlopeAngle - slopeThreshold));
                    speed *= percent;
                }
            }

            currentMoveVelocity = speed * moveDir;
        }

        public static void CalculateMoveVelocity(float delta, float acceleration, float deceleration, Rigidbody body, ref Vector3 currentMoveVector, Vector3 moveInput, float maxSpeed, bool isOnFloor, RaycastHit hitInfo, float slopeThreshold, float maxSlopeAngle)
        {
            // Convert input with current body rotation to get actuall direction to move in
            Quaternion yawRotation = Quaternion.Euler(0f, body.rotation.eulerAngles.y, 0f);
            Vector3 moveInputDir = yawRotation * moveInput;

            if (isOnFloor)
            {
                moveInputDir = Vector3.ProjectOnPlane(moveInputDir, hitInfo.normal).normalized;
                float angle = TDVector.GetAngleToGroundPlane(moveInputDir, Vector3.up);

                if (angle > slopeThreshold)
                {
                    float percent = 1 - ((angle - slopeThreshold) / (maxSlopeAngle - slopeThreshold));
                    maxSpeed *= percent;
                    acceleration *= percent;
                }
            }
            
            if (moveInput.sqrMagnitude == 0)
            {
                Decel(ref currentMoveVector, hitInfo, deceleration, delta);
            }
            else
            {
                // if (isMoving && not inputDirection(Matches)moveDirection)
                if (currentMoveVector.sqrMagnitude > 0.01f && Vector3.Dot(currentMoveVector.normalized, moveInputDir) < 0.5f)
                {
                    Decel(ref currentMoveVector, hitInfo, acceleration, delta);
                }
                else
                {
                    //accelerate
                    float newMoveVelocity = Mathf.Min(currentMoveVector.magnitude + (acceleration * delta), maxSpeed);
                    currentMoveVector = newMoveVelocity * moveInputDir;
                }
            }
        }

        private static void Decel(ref Vector3 currentMoveVector, RaycastHit hitInfo, float acceleration, float delta)
        {
            float newMoveVelocity = Mathf.Max(0, currentMoveVector.magnitude - (acceleration * delta));
            currentMoveVector = newMoveVelocity * currentMoveVector.normalized;

            currentMoveVector = Vector3.ProjectOnPlane(currentMoveVector, hitInfo.normal);
        }

        public static void Look(Vector2 lookInput, Rigidbody body, float sensitivity, Transform headTransform)
        {
            body.rotation = Quaternion.Euler(new Vector3(0, body.rotation.eulerAngles.y + (lookInput.x * sensitivity), 0));

            float currentRotation = headTransform.localEulerAngles.x;
            float newRotation = currentRotation + (-lookInput.y * sensitivity);

            if (newRotation > 180)
            {
                if (newRotation < 280)
                {
                    newRotation = 280;
                }
            }
            else if (newRotation > 85)
            {
                newRotation = 85;
            }

            headTransform.localRotation = Quaternion.Euler(new Vector3(newRotation, 0, 0));
        }
    }
}
