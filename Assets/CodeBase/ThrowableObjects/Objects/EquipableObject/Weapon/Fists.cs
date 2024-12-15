using CodeBase.Character;
using CodeBase.Character.Interfaces;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public class Fists : Weapon
    {
        protected override Collider2D[] FindTargets(Vector2 attackDirection)
        {
            Vector2 scale = new(_attackRange, _attackRange);
            Vector2 attackPoint = (Vector2)transform.position + attackDirection.normalized * scale / 2;

            DrawDebugBox(attackPoint, scale);
            return Physics2D.OverlapBoxAll(attackPoint, scale, 0, _enemyMask);
        }

        protected override void CalcDurability()
        {
            return;
        }
        private protected override void DealDamage(IWeaponDamageable target, AttackAnimationInfo animInfo)
        {
            target.TakeDamageFromFists(_damage, animInfo.HitDelay, animInfo.HitInterval);
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