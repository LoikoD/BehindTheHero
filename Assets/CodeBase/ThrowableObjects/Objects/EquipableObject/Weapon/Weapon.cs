using CodeBase.Character;
using CodeBase.EnemiesScripts.Controller;
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

        public void Attack(Vector2 attackDirection, AttackAnimationInfo animInfo)
        {
            _hitColliders = FindTargets(transform.position, attackDirection, _enemyMask);

            if (_hitColliders.Length > 0)
            {
                foreach (var hit in _hitColliders)
                {
                    if (hit.gameObject.TryGetComponent<IWeaponDamageable>(out var enemy))
                    {
                        if (this is Fists)
                        {
                            enemy.TakeDamageFromFists(_damage, animInfo.HitDelay, animInfo.HitInterval);
                        }
                        else
                        {
                            enemy.TakeDamageFromWeapon(_damage, animInfo.HitDelay);
                        }
                    }
                }
                
                CalcDurability();
            }

        }
    }
}