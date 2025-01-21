using UnityEngine;

namespace CodeBase.Logic.Utilities
{
    public class ClosestOnScreenTargetFinder
    {
        private readonly LayerMask _layerMask;
        private readonly Camera _camera;
        private Collider2D[] _hitColliders;

        public ClosestOnScreenTargetFinder(LayerMask layerMask)
        {
            _layerMask = layerMask;
            _camera = Camera.main;
        }

        public bool TryFindTarget(Vector2 currentPosition, out Transform target)
        {
            Bounds bounds = GetScreenBounds();
            _hitColliders = Physics2D.OverlapAreaAll(bounds.min, bounds.max, _layerMask);

            float closestDistance = float.MaxValue;
            Transform closestTarget = null;

            foreach (var hit in _hitColliders)
            {
                float distance = Vector2.Distance(currentPosition, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }

            target = closestTarget;
            return closestTarget != null;
        }

        private Bounds GetScreenBounds()
        {
            float cameraHeight = _camera.orthographicSize * 2;
            float cameraWidth = cameraHeight * _camera.aspect;
            return new Bounds(_camera.transform.position, new Vector2(cameraWidth, cameraHeight));
        }
    }
}