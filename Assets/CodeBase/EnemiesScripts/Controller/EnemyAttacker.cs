using CodeBase.Character;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemyAttacker : CharacterAttacker
    {
        private float _damage;
        private float _attackCooldown;

        internal override float AttackCooldown { get => _attackCooldown; }

        public void Construct(EnemyAnimationsController enemyAnimator, float damage, float attackCooldown)
        {
            Construct(enemyAnimator);

            _damage = damage;
            _attackCooldown = attackCooldown;
        }
    
        internal override void DoAttack(Transform target)
        {
            target.TryGetComponent(out IHealth knight);
            knight.TakeDamage(_damage);
        }
    }
}
