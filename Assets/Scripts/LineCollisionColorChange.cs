using UnityEngine;

public class LineCollisionColorChange : MonoBehaviour
{
    public Color originalColor;
    public Color collisionColor;
    public LineRenderer lineRenderer;
    public float collisionDistanceThreshold = 0.1f; // Adjust this based on the line width and sphere size
    private Renderer sphereRenderer;

    void Start()
    {
        // Get the Renderer component from the sphere
        sphereRenderer = GetComponent<Renderer>();
        // Set the sphere's original color
        sphereRenderer.material.color = originalColor;
    }

    void Update()
    {
        if (IsLineRendererIntersecting())
        {
            sphereRenderer.material.color = collisionColor;
            Debug.Log("Color is being changed");
            Data_Exp3.numberNTryReentryGaze++;
            Data_Exp3.numberSceneReentryGaze++;
        }
        else
        {
            sphereRenderer.material.color = originalColor;
        }
    }

    bool IsLineRendererIntersecting()
    {
        if (lineRenderer == null)
        {
            return false;
        }

        Vector3 spherePosition = transform.position;
        float sphereRadius = transform.localScale.x / 2;

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 start = lineRenderer.GetPosition(i);
            Vector3 end = lineRenderer.GetPosition(i + 1);

            if (IsSphereIntersectingLineSegment(spherePosition, sphereRadius, start, end))
            {
                return true;
            }
        }

        return false;
    }

    bool IsSphereIntersectingLineSegment(Vector3 sphereCenter, float sphereRadius, Vector3 start, Vector3 end)
    {
        Vector3 closestPoint = ClosestPointOnLineSegment(sphereCenter, start, end);
        float distance = Vector3.Distance(sphereCenter, closestPoint);
        return distance < sphereRadius + collisionDistanceThreshold;
    }

    Vector3 ClosestPointOnLineSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 lineDirection = end - start;
        float lineLength = lineDirection.magnitude;
        lineDirection.Normalize();

        float projectLength = Vector3.Dot(point - start, lineDirection);
        projectLength = Mathf.Clamp(projectLength, 0, lineLength);

        return start + lineDirection * projectLength;
    }
}
