using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public abstract class Weapon : ThrowableObject
    {
        [SerializeField] internal Transform _attackArea;
        [SerializeField] internal float _damage;
        [SerializeField] internal float _durability;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private LayerMask _enemyMask;

        private Collider2D[] _hitColliders;

        internal readonly float _durabilityChangeStep = 1f;

        public float CurrentDurability { get; set; }
        public float MaxDurability => _durability;
        public float AttackCooldown => _attackCooldown;


        protected abstract Collider2D[] FindTargets(Vector2 attackerPosition, Vector2 attackDirection, LayerMask mask);

        protected virtual void CalcDurability()
        {
            CurrentDurability -= _durabilityChangeStep;
        }

        public void Attack(Vector2 attackerPosition, Vector2 attackDirection)
        {
            _hitColliders = FindTargets(transform.position, attackDirection, _enemyMask);

            if (_hitColliders.Length > 0)
            {
                foreach (var hit in _hitColliders)
                {
                    if (hit.gameObject.TryGetComponent<IHealth>(out var enemy))
                    {
                        //Debug.Log($"{hit.gameObject.name} took {_damage} damage");
                        enemy.TakeDamage(_damage);
                    }
                }
                
                CalcDurability();
            }

        }
    }
}