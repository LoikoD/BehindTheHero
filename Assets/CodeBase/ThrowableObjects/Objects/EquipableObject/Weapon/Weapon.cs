using System.Collections;
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
        private bool _isOnCooldown;

        public float CurrentDurability { get; set; }
        public float MaxDurability => _durability;
        public bool IsOnCooldown => _isOnCooldown;


        protected abstract Collider2D[] FindTargets(Vector2 attackerPosition, Vector2 attackDirection, LayerMask mask);

        protected virtual void CalcDurability()
        {
            CurrentDurability -= _durabilityChangeStep;
            if (CurrentDurability <= 0)
            {
                _isOnCooldown = false;
            }
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
            
            StartCoroutine(AttackCoroutine());
        }
        
        private IEnumerator AttackCoroutine()
        {
            _isOnCooldown = true;

            yield return new WaitForSeconds(_attackCooldown);

            _isOnCooldown = false;
        }
    }
}