using CodeBase.Character;
using CodeBase.Character.Interfaces;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private protected float _attackRange;
        [SerializeField] private protected float _damage;
        [SerializeField] private float _durability;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private protected LayerMask _enemyMask;

        private readonly float _durabilityChangeStep = 1f;

        public float CurrentDurability { get; set; }
        public float MaxDurability => _durability;
        public float AttackCooldown => _attackCooldown;


        protected abstract Collider2D[] FindTargets(Vector2 attackDirection);

        protected virtual void CalcDurability()
        {
            CurrentDurability -= _durabilityChangeStep;
        }

        private protected virtual void DealDamage(IWeaponDamageable target, AttackAnimationInfo animInfo)
        {
            target.TakeDamageFromWeapon(_damage, animInfo.HitDelay);
        }

        public void Attack(Vector2 attackDirection, AttackAnimationInfo animInfo)
        {
            var _hitColliders = FindTargets(attackDirection);

            if (_hitColliders.Length > 0)
            {
                foreach (var hit in _hitColliders)
                {
                    if (hit.gameObject.TryGetComponent<IWeaponDamageable>(out var enemy))
                    {
                        DealDamage(enemy, animInfo);
                    }
                }
                
                CalcDurability();
            }
        }
    }
}