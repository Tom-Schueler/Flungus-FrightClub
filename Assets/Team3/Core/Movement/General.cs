using UnityEngine;

namespace Team3.Movement
{
    public static class GeneralMovement
    {
        public static void CalculateMoveVelocity(ref Vector3 currentMoveVelocity, Vector3 moveInput, float speed, bool isOnFloor, RaycastHit hitInfo)
        {
            if (isOnFloor)
            {
                moveInput = Vector3.ProjectOnPlane(moveInput, hitInfo.normal).normalized;
            }

            currentMoveVelocity = speed * moveInput;
        }

        // public static void CalculateMoveVelocityWihtAccelerationTime()

        // public static void CalculateMoveVelocityWithAcceleration()

        public static void Look(Vector2 lookInput, float minLookInput, Rigidbody body)
        {
            if (lookInput.magnitude < minLookInput) { return; }

            float angle = Mathf.Atan2(lookInput.x, lookInput.y) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            body.rotation = rotation;
        }

        public static void CalculateFallVelocity(float delta, ref float currentVerticalVelocity, float gravity, float terminalVelocity)
        {
            currentVerticalVelocity -= gravity * delta;
            currentVerticalVelocity = Mathf.Max(currentVerticalVelocity, -terminalVelocity);
        }

        public static void CalculateJumpTakeofVelocity(float takeofSpeed, ref float currentVerticalVelocity)
        {
            currentVerticalVelocity += takeofSpeed;

            if (currentVerticalVelocity > takeofSpeed)
            {
                currentVerticalVelocity -= takeofSpeed / 3;
            }
        }

        /// <summary>
        /// Note that origin is global origin
        /// </summary>
        /// <param name="body"></param>
        /// <param name="sphereRadius"></param>
        /// <param name="origin"></param>
        /// <param name="maxDistance"></param>
        /// <param name="ground"></param>
        /// <param name="hitInfo"></param>
        /// <returns></returns>
        public static bool CheckIfOnFloor(float sphereRadius, Vector3 origin, float maxDistance, LayerMask ground, out RaycastHit hitInfo)
        {
            return Physics.SphereCast(origin, sphereRadius, Vector3.down, out hitInfo, maxDistance, ground, QueryTriggerInteraction.Ignore);
        }


        public static float GetSlopeAngle(RaycastHit hitInfo, Vector3 upVector)
        {
            return Vector3.Angle(hitInfo.normal, upVector);
        }
    }
}
