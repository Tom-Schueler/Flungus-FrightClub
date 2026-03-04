using UnityEngine;

namespace Team3.Multiplayer.EnemyCamp
{
    public class CampSpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyCamp enemyCamp;

        private void OnDrawGizmos()
        {
            if (enemyCamp == null)
            { return; }

            
            Color color = Color.red;
            color.a = 0.2f;
            Gizmos.color = color;

            Gizmos.DrawSphere(transform.position + enemyCamp.Center, enemyCamp.Radius);


            Gizmos.color = Color.yellow;

            Vector3 center = transform.position;
            center.y -= enemyCamp.GroundCheckLength / 2f;
            Vector3 topCenter = center + Vector3.up * (enemyCamp.GroundCheckLength / 2f);
            Vector3 bottomCenter = center - Vector3.up * (enemyCamp.GroundCheckLength / 2f);

            int segments = 32;
            float angleStep = 360f / segments;

            Vector3[] topCircle = new Vector3[segments];
            Vector3[] bottomCircle = new Vector3[segments];

            // Generate circle points
            for (int i = 0; i < segments; i++)
            {
                float angle = Mathf.Deg2Rad * angleStep * i;
                float x = Mathf.Cos(angle) * enemyCamp.SpawnRadius;
                float z = Mathf.Sin(angle) * enemyCamp.SpawnRadius;

                topCircle[i] = topCenter + new Vector3(x, 0, z);
                bottomCircle[i] = bottomCenter + new Vector3(x, 0, z);
            }

            // Draw edges + top & bottom faces
            for (int i = 0; i < segments; i++)
            {
                int next = (i + 1) % segments;

                // Circle edge lines
                Gizmos.DrawLine(topCircle[i], topCircle[next]);       // Top ring
                Gizmos.DrawLine(bottomCircle[i], bottomCircle[next]); // Bottom ring

                // Side lines
                Gizmos.DrawLine(topCircle[i], bottomCircle[i]);

                // Top face (triangle fan lines)
                Gizmos.DrawLine(topCenter, topCircle[i]);
                // Bottom face (triangle fan lines)
                Gizmos.DrawLine(bottomCenter, bottomCircle[i]);
            }
        }
    }
}
