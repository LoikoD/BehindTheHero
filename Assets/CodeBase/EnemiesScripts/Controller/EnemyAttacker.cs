using CodeBase.Character;
using CodeBase.Character.Interfaces;
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
    
        internal override void DoAttack(Transform target, float hitInterval)
        {
            IDamageable knight = target.GetComponent<IDamageable>();
            knight?.TakeDamage(_damage);
        }
    }
}
