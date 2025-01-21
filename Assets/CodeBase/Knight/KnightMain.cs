using System;
using CodeBase.Character;
using CodeBase.Character.CharacterFSM;
using CodeBase.Character.Interfaces;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightMain : CharacterMain, IDamageable
    {
        [SerializeField] private Transform _centerPos;

        public Transform CenterPos => _centerPos;

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