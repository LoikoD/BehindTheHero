using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Logic.Utilities
{
    public class OffScreenPointProvider
    {
        private readonly Camera _camera;
        private readonly float _spawnDistanceFromEdge;

        public OffScreenPointProvider(Camera camera, float spawnDistanceFromEdge)
        {
            _camera = camera;
            _spawnDistanceFromEdge = spawnDistanceFromEdge;
        }

        public List<Vector2> GetRandomGroupPoints(int count, float minSpreadDistance)
        {
            List<Vector2> points = new();

            float baseAngle = Random.Range(0, 360);
            points.Add(GetPoint(baseAngle));

            if (count == 1) return points;

            Vector2 center = Camera.main.transform.position;

            float maxSpreadAngle = count * 360f / 100f;
            maxSpreadAngle = Mathf.Clamp(maxSpreadAngle, 5, 360);

            float startAngle = baseAngle - (maxSpreadAngle * 0.5f);

            for (int i = 1; i < count; i++)
            {
                float angle = startAngle + Random.Range(0f, maxSpreadAngle);

                Vector2 pointOnBoundary = GetPoint(angle);
                Vector2 direction = (pointOnBoundary - center).normalized;
                pointOnBoundary += direction * Random.Range(0, minSpreadDistance);

                Vector2 point = ApplyMinDistance(pointOnBoundary, direction, points.Take(i).ToList(), minSpreadDistance);

                points.Add(point);
            }
            return points;
        }

        public Vector2 GetRandomPoint()
        {
            float angle = Random.Range(0f, 360f);
            return GetPoint(angle);
        }

        private Vector2 GetPoint(float angle)
        {
            Vector2 direction = new(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            Vector2 edgeIntersection = GetEdgeIntersectionPoint(direction);
            return edgeIntersection + direction * _spawnDistanceFromEdge;
        }

        private Vector2 GetEdgeIntersectionPoint(Vector2 direction)
        {
            float screenHeight = _camera.orthographicSize * 2;
            float screenWidth = screenHeight * _camera.aspect;
            Bounds screenBounds = new(_camera.transform.position, new Vector2(screenWidth, screenHeight));

            float distanceToVertical = GetDistanceToEdge(direction.x, screenBounds.extents.x);
            float distanceToHorizontal = GetDistanceToEdge(direction.y, screenBounds.extents.y);
            float distanceToEdge = Mathf.Min(distanceToVertical, distanceToHorizontal);

            return (Vector2)screenBounds.center + direction * distanceToEdge;
        }

        private float GetDistanceToEdge(float direction, float extent)
        {
            if (direction == 0)
                return float.MaxValue;

            return extent / Mathf.Abs(direction);
        }

        private Vector2 ApplyMinDistance(Vector2 point, Vector2 pointDirection, List<Vector2> finalPoints, float requiredMinDistance, int recursionDepth = 0)
        {
            const int MaxRecursion = 10;
            if (recursionDepth >= MaxRecursion)
            {
                Debug.LogWarning($"Max recursion depth reached in ApplyMinDistance; point={point}; dir={pointDirection}");
                return point;
            }

            float minDistance = float.MaxValue;
            Vector2 closestPoint = Vector2.zero;
            foreach (var prevPoint in finalPoints)
            {
                float distance = Vector2.Distance(point, prevPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = prevPoint;
                }
            }

            Vector2 newPoint = point;
            if (requiredMinDistance - minDistance > 0.01f)
            {
                Vector2 toClosestPoint = closestPoint - point;
                float angleBetween = Vector2.Angle(pointDirection, toClosestPoint) * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angleBetween);
                float a = 1f;
                float b = -2f * minDistance * cos;
                float c = minDistance * minDistance - requiredMinDistance * requiredMinDistance;

                float discriminant = b * b - 4f * a * c;
                if (discriminant >= 0)
                {
                    float offset = (-b + Mathf.Sqrt(discriminant)) / (2f * a);
                    newPoint += pointDirection * offset;
                }
                else
                {
                    Debug.LogError("discriminant < 0; should not be possible");
                    newPoint += pointDirection * (requiredMinDistance - minDistance);
                }
                newPoint = ApplyMinDistance(newPoint, pointDirection, finalPoints, requiredMinDistance, recursionDepth + 1);
            }
            return newPoint;
        }

        public List<Vector2> GetPointsForDebug()
        {
            List<Vector2> points = new();
            for (int i = 0; i < 360; i++)
            {
                points.Add(GetPoint(i));
            }
            return points;
        }
    }
}
