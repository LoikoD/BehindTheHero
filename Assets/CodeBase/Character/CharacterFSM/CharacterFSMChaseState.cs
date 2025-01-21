using CodeBase.Character.Interfaces;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterFSMChaseState : IFsmState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly IMover _mover;
        private readonly IAnimationsController _animator;
        private readonly CharacterStaticData _data;

        public CharacterFSMChaseState(CharacterStateMachine stateMachine, IMover mover, IAnimationsController animator, CharacterStaticData data)
        {
            _stateMachine = stateMachine;
            _mover = mover;
            _animator = animator;
            _data = data;
        }

        public void Enter()
        {
            _animator.Run();
        }

        public void Update()
        {
            if (_mover.DistanceToTarget(_stateMachine.Target) <= _data.AttackRange)
            {
                _stateMachine.SetState<CharacterFSMAttackState>();
            }

            _mover.Move(_stateMachine.Target);
        }

        public void Exit()
        {

        }
    }
}
