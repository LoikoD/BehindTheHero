using System;
using CodeBase.Character;
using CodeBase.Character.CharacterFSM;
using CodeBase.Character.Interfaces;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemyMain : CharacterMain, IWeaponDamageable
    {
        private EnemyAnimationsController _enemyAnimator;

        public event Action<EnemyMain> Died;

        public override void Construct(CharacterStateMachine stateMachine, IAnimationsController animator, float health)
        {
            base.Construct(stateMachine, animator, health);
            _enemyAnimator = (EnemyAnimationsController)animator;
        }
        public void ResetState()
        {
            CurrentHealth = MaxHealth;

            _stateMachine.Reset();
        }

        public void TakeDamageFromFists(float damage, float interval)
        {
            if (_stateMachine.HasDied)
                return;

            _enemyAnimator.TakeDamageFromFists(interval);

            BaseTakeDamage(damage);
        }
        public void TakeDamageFromWeapon(float damage)
        {
            if (_stateMachine.HasDied)
                return;

            _enemyAnimator.TakeDamageFromWeapon();

            BaseTakeDamage(damage);
        }

        internal override void Dead()
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}