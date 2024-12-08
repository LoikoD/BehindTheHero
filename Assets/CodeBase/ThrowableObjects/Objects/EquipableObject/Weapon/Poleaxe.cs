using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public class Poleaxe : Weapon
    {
        private CircleCollider2D _attackAreaCollider;

        private void Awake()
        {
            _attackAreaCollider = _attackArea.GetComponent<CircleCollider2D>();
        }

        protected override Collider2D[] FindTargets(Vector2 attackerPosition, Vector2 attackDirection, LayerMask mask)
        {
            float scale = _attackArea.localScale.x * transform.localScale.x * _attackAreaCollider.radius;

            DrawDebugCircle(attackerPosition, scale);
            return Physics2D.OverlapCircleAll(attackerPosition, scale, mask);
        }

        private void DrawDebugCircle(Vector2 center, float radius, int segments = 36)
        {
            float angleStep = 360f / segments;
            for (int i = 0; i < segments; i++)
            {
                float angle1 = Mathf.Deg2Rad * (i * angleStep);
                float angle2 = Mathf.Deg2Rad * ((i + 1) * angleStep);

                Vector2 point1 = center + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
                Vector2 point2 = center + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;

                Debug.DrawLine(point1, point2, Color.green, AttackCooldown);
            }
        }
    }
}