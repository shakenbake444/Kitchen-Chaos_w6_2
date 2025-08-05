using UnityEngine;

[ExecuteAlways]
public class CapsuleCastVisualizer : MonoBehaviour
{
    public float height = 2f;
    public float radius = 0.5f;
    public Vector3 direction = Vector3.forward;
    public float _speed = 5f; // Player movement speed
    public bool drawInPlayModeOnly = false;

    void OnDrawGizmos()
    {
        if (drawInPlayModeOnly && !Application.isPlaying)
            return;

        float moveDistance = _speed * Time.deltaTime;

        // Define capsule points relative to player position
        Vector3 p1 = transform.position + Vector3.up;
        Vector3 p2 = transform.position + Vector3.up * height;

        // Direction in world space
        Vector3 castDir = transform.TransformDirection(direction).normalized;

        // Original capsule
        Gizmos.color = Color.green;
        DrawWireCapsule(p1, p2, radius);

        // Destination capsule (casted position)
        Vector3 offset = castDir * moveDistance;
        Gizmos.color = Color.cyan;
        DrawWireCapsule(p1 + offset, p2 + offset, radius);

        // Cast path line
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine((p1 + p2) * 0.5f, (p1 + p2) * 0.5f + offset);

        // Perform the actual CapsuleCast
        if (Physics.CapsuleCast(p1, p2, radius, castDir, out RaycastHit hit, moveDistance))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.1f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(hit.point, hit.normal * 0.5f);

            Gizmos.color = Color.white;
            Debug.DrawLine((p1 + p2) * 0.5f, hit.point, Color.white);
        }
    }

    void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius)
    {
        if (p1 == p2)
        {
            Gizmos.DrawWireSphere(p1, radius);
            return;
        }

        Vector3 up = (p2 - p1).normalized;
        float height = Vector3.Distance(p1, p2);
        float cylinderHeight = height - 2 * radius;
        if (cylinderHeight < 0f) cylinderHeight = 0f;

        Gizmos.DrawWireSphere(p1, radius);
        Gizmos.DrawWireSphere(p2, radius);

        int segments = 16;
        for (int i = 0; i < segments; i++)
        {
            float angle1 = (i / (float)segments) * Mathf.PI * 2f;
            float angle2 = ((i + 1) / (float)segments) * Mathf.PI * 2f;

            Vector3 offset1 = new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1)) * radius;
            Vector3 offset2 = new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2)) * radius;

            Quaternion rotation = Quaternion.LookRotation(up);
            offset1 = rotation * offset1;
            offset2 = rotation * offset2;

            Vector3 start1 = p1 + offset1;
            Vector3 start2 = p2 + offset1;
            Vector3 end1 = p1 + offset2;
            Vector3 end2 = p2 + offset2;

            Gizmos.DrawLine(start1, start2);
            Gizmos.DrawLine(start1, end1);
            Gizmos.DrawLine(start2, end2);
        }
    }
}
