using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public class Sword : Weapon
    {
        private BoxCollider2D _attackAreaCollider;

        private void Awake()
        {
            _attackAreaCollider = _attackArea.GetComponent<BoxCollider2D>();
        }

        protected override Collider2D[] FindTargets(Vector2 attackerPosition, Vector2 attackDirection, LayerMask mask)
        {
            Vector2 scale = new Vector2(_attackArea.localScale.x * transform.localScale.x, _attackArea.localScale.y * transform.localScale.y) * _attackAreaCollider.size;
            Vector2 attackPoint = attackerPosition + attackDirection.normalized * scale / 2;
            //Debug.Log($"{attackPoint}; {scale}; {attackDirection.normalized}");

            return Physics2D.OverlapBoxAll(attackPoint, scale, 0, mask);
        }
    }
}