using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public class Sword : Weapon
    {
        protected override Collider2D[] FindTargets(Vector2 attackDirection)
        {
            Vector2 scale = new(_attackRange, _attackRange);
            Vector2 attackPoint = (Vector2)transform.position + attackDirection.normalized * scale / 2;

            DrawDebugBox(attackPoint, scale);
            return Physics2D.OverlapBoxAll(attackPoint, scale, 0, _enemyMask);
        }

        private void DrawDebugBox(Vector2 center, Vector2 size)
        {
            Vector2 halfSize = size / 2;

            Vector2 topLeft = center + new Vector2(-halfSize.x, halfSize.y);
            Vector2 topRight = center + new Vector2(halfSize.x, halfSize.y);
            Vector2 bottomLeft = center + new Vector2(-halfSize.x, -halfSize.y);
            Vector2 bottomRight = center + new Vector2(halfSize.x, -halfSize.y);

            Debug.DrawLine(topLeft, topRight, Color.blue, AttackCooldown);
            Debug.DrawLine(topRight, bottomRight, Color.blue, AttackCooldown    );
            Debug.DrawLine(bottomRight, bottomLeft, Color.blue, AttackCooldown);
            Debug.DrawLine(bottomLeft, topLeft, Color.blue, AttackCooldown);
        }
    }
}