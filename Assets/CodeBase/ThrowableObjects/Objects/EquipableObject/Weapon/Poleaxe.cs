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

            return Physics2D.OverlapCircleAll(attackerPosition, scale, mask);
        }
    }
}