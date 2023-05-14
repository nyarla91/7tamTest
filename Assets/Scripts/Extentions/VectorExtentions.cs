using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extentions
{
    public static class VectorExtentions
    {
        public static Vector2 WithX(this Vector2 vector, float newX) => new Vector2(newX, vector.y);

        public static Vector2 WithY(this Vector2 vector, float newY) => new Vector2(vector.x, newY);
        
        public static Vector3 WithZ(this Vector2 vector, float newZ) => new Vector3(vector.x, vector.y, newZ);
        public static Vector3 WithX(this Vector3 vector, float newX) => new Vector3(newX, vector.y, vector.z);
 
        public static Vector3 WithY(this Vector3 vector, float newY) => new Vector3(vector.x, newY, vector.z);

        public static Vector3 WithZ(this Vector3 vector, float newZ) => new Vector3(vector.x, vector.y, newZ);
        
        public static Vector2 DegreesToVector2(this float z) =>
            new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));

        public static float ToDegrees(this Vector2 vector)
        {
            vector.Normalize();
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }

        public static void Rotate(ref Vector2 vector, float degrees)
        {
            float oldDegrees = ToDegrees(vector);
            float oldDistance = vector.magnitude;
            vector = DegreesToVector2(oldDegrees + degrees) * oldDistance;
        }

        public static Vector2 Rotated(this Vector2 vector, float degrees)
        {
            Rotate(ref vector, degrees);
            return vector;
        }

        public static Vector2 RandomPointInBounds2D(this Bounds bounds)
        {
            Vector2 max = bounds.max;
            Vector2 min = bounds.min;
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }

        public static Vector3 RandomPointInBounds(this Bounds bounds)
        {
            Vector3 max = bounds.max;
            Vector3 min = bounds.min;
            return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }
    }

}