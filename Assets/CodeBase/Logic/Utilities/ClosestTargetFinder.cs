using UnityEngine;

namespace CodeBase.Logic.Utilities
{
    public class ClosestTargetFinder
    {
        private float _radius;
        private LayerMask _layerMask;
        private Collider2D[] _hitColliders;

        public ClosestTargetFinder(float radius, LayerMask layerMask)
        {
            _radius = radius;
            _layerMask = layerMask;
        }

        public bool TryFindTarget(Vector2 currentPosition, out IHealth target)
        {
            _hitColliders = Physics2D.OverlapCircleAll(currentPosition, _radius, _layerMask);

            if (_hitColliders.Length > 0)
            {
                foreach (var hit in _hitColliders)
                {
                    if (hit.transform.gameObject.TryGetComponent(out IHealth enemy))
                    {
                        target = enemy;

                        return true;
                    }
                }
            }

            target = null;

            return false;
        }
    }
}