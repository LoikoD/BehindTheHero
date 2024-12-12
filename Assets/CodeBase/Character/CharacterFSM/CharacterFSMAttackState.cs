using CodeBase.Character.Interfaces;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterFSMAttackState : IFsmState
    {
        internal readonly CharacterStateMachine _stateMachine;
        private readonly CharacterAttacker _attacker;
        private readonly CharacterStaticData _data;
        private readonly IAnimationsController _animator;
        private float _distance;

        public CharacterFSMAttackState(CharacterStateMachine stateMachine, CharacterAttacker attacker, IAnimationsController animator, CharacterStaticData data)
        {
            _stateMachine = stateMachine;
            _attacker = attacker;
            _animator = animator;
            _data = data;
        }

        public void Enter()
        {
            _animator.Idle();
        }

        public void Update()
        {
            if (NeedChaseTarget())
            {
                _stateMachine.SetState<CharacterFSMChaseState>();
            }
            else
            {
                _attacker.Attack(_stateMachine.Target.Transform);
            }

            CheckTarget();
        }

        internal virtual void CheckTarget()
        {
            return;
        }

        public void Exit()
        {
        }

        private bool NeedChaseTarget()
        {
            _distance = Vector3.Distance(_attacker.transform.position, _stateMachine.Target.Transform.position);

            if (_distance > _data.AttackRange)
                return true;

            return false;
        }
    }
}
