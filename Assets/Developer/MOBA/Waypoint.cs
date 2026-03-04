using UnityEngine;

namespace Team3.Enemys
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] 
        private Color gizmoColor = Color.white;

        [SerializeField]
        private float debugDrawRadius = 5.0f;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, debugDrawRadius);
        }
    }
}