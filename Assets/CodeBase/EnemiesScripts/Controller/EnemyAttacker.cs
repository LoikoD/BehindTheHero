using CodeBase.Character;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemyAttacker : CharacterAttacker
    {
        private float _damage;
        private float _attackCooldown;
        private float _radius;
        private LayerMask _layer;

        internal override float AttackCooldown { get => _attackCooldown; }

        public void Construct(EnemyAnimationsController enemyAnimator, float damage, float attackCooldown, float attackRadius, LayerMask knightLayer)
        {
            Construct(enemyAnimator);

            _damage = damage;
            _attackCooldown = attackCooldown;
            _radius = attackRadius;
            _layer = knightLayer;
        }
    
        internal override void DoAttack(Transform target)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _layer);
            
            if (hit != null)
            {
                hit.TryGetComponent(out IHealth knight);
                knight.TakeDamage(_damage);
            }
        }
    }
}
