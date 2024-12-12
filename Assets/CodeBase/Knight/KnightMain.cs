using System;
using CodeBase.Character;
using CodeBase.Character.CharacterFSM;
using CodeBase.Character.Interfaces;

namespace CodeBase.Knight
{
    public class KnightMain : CharacterMain, IDamageable
    {
        private KnightAnimationsController _knightAnimator;
        public event Action Died;

        public override void Construct(CharacterStateMachine stateMachine, IAnimationsController animator, float health)
        {
            base.Construct(stateMachine, animator, health);
            _knightAnimator = (KnightAnimationsController)animator;
        }

        public void TakeDamage(float damage)
        {
            if (_stateMachine.HasDied)
                return;

            _knightAnimator.TakeDamage();

            BaseTakeDamage(damage);

        }
        internal override void Dead()
        {
            Died?.Invoke();
        }
    }
}