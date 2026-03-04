using UnityEngine;

namespace Team3.CustomMath
{
    public static class TDVector
    {
        public static float GetAngleToGroundPlane(Vector3 vector, Vector3 planeNormal)
        {
            float angleToNormal = Vector3.Angle(vector, planeNormal);
            return 90f - angleToNormal;
        }
    }
}
