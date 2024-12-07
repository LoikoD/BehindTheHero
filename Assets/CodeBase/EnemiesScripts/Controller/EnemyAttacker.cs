using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        private float _damage;
        private float _attackCooldown;
        private float _radius;
        private LayerMask _layer;
        private bool _isOnCooldown = false;
        private EnemyAnimationsController _enemyAnimationsController;

        private void Awake()
        {
            _enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        }

        public void Construct(float damage, float attackCooldown, float attackRadius, LayerMask knightLayer)
        {
            _damage = damage;
            _attackCooldown = attackCooldown;
            _radius = attackRadius;
            _layer = knightLayer;
        }
    
        public void Attack()
        {
            if (_isOnCooldown)
                return;
        
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _layer);
            
            if (hit != null)
            {
                hit.TryGetComponent<IHealth>(out IHealth knight);
            
                knight.TakeDamage(_damage);

                _enemyAnimationsController.Attack();
                StartCoroutine(AttackCoroutine());
            }
        }

        private IEnumerator AttackCoroutine()
        {
            _isOnCooldown = true;

            yield return new WaitForSeconds(_attackCooldown);

            _isOnCooldown = false;
        }
    }
}
