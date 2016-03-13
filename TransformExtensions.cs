using UnityEngine;

public static class TransformExtensions
{
    public static void ClampTranslate(this Transform transform, Vector3 translation, Vector3 min, Vector3 max)
    {
        var newPosition = transform.position + translation;

        if (newPosition.x < min.x) newPosition.x = min.x;
        if (newPosition.y < min.y) newPosition.y = min.y;
        if (newPosition.z < min.z) newPosition.z = min.z;

        if (newPosition.x > max.x) newPosition.x = max.x;
        if (newPosition.y > max.y) newPosition.y = max.y;
        if (newPosition.z > max.z) newPosition.z = max.z;

        transform.position = newPosition;
    }
}