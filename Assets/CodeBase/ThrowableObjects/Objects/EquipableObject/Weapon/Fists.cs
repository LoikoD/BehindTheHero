using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public class Fists : Weapon
    {
        private BoxCollider2D _attackAreaCollider;

        private void Awake()
        {
            _attackAreaCollider = GetComponentInChildren<BoxCollider2D>();
        }

        protected override Collider2D[] FindTargets(Vector2 attackerPosition, Vector2 attackDirection, LayerMask mask)
        {
            Vector2 scale = new Vector2(_attackArea.localScale.x * transform.localScale.x, _attackArea.localScale.y * transform.localScale.y) * _attackAreaCollider.size;
            Vector2 attackPoint = attackerPosition + attackDirection.normalized * scale / 2;

            DrawDebugBox(attackPoint, scale);
            return Physics2D.OverlapBoxAll(attackPoint, scale, 0, mask);
        }

        protected override void CalcDurability()
        {
            return;
        }

        private void DrawDebugBox(Vector2 center, Vector2 size)
        {
            Vector2 halfSize = size / 2;

            Vector2 topLeft = center + new Vector2(-halfSize.x, halfSize.y);
            Vector2 topRight = center + new Vector2(halfSize.x, halfSize.y);
            Vector2 bottomLeft = center + new Vector2(-halfSize.x, -halfSize.y);
            Vector2 bottomRight = center + new Vector2(halfSize.x, -halfSize.y);

            Debug.DrawLine(topLeft, topRight, Color.red, AttackCooldown);
            Debug.DrawLine(topRight, bottomRight, Color.red, AttackCooldown);
            Debug.DrawLine(bottomRight, bottomLeft, Color.red, AttackCooldown);
            Debug.DrawLine(bottomLeft, topLeft, Color.red, AttackCooldown);
        }
    }
}