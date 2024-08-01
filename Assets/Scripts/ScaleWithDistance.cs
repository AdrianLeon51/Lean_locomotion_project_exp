using UnityEngine;

public class ScaleWithDistance : MonoBehaviour
{
    public Transform player; // The player's transform
    public float minScale = 0.1f; // Minimum scale factor
    public float maxScale = 1.0f; // Maximum scale factor
    public float minDistance = 1.0f; // Minimum distance for scaling
    public float maxDistance = 10.0f; // Maximum distance for scaling

    void Update()
    {
        // Calculate the distance between the player and the object
        float distance = Vector3.Distance(player.position, transform.position);

        // Clamp the distance within the defined range
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate the scale factor based on the distance
        float scale = Mathf.Lerp(maxScale, minScale, (distance - minDistance) / (maxDistance - minDistance));

        // Apply the scale to the object
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
