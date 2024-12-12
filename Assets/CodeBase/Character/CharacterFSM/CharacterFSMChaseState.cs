using CodeBase.Character.Interfaces;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterFSMChaseState : IFsmState
    {
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterMover _mover;
        private readonly IAnimationsController _animator;
        private readonly CharacterStaticData _data;

        public CharacterFSMChaseState(CharacterStateMachine stateMachine, CharacterMover mover, IAnimationsController animator, CharacterStaticData data)
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
            if (Vector3.Distance(_mover.transform.position, _stateMachine.Target.Transform.position) <= _data.AttackRange)
            {
                _stateMachine.SetState<CharacterFSMAttackState>();
            }

            _mover.Move(_stateMachine.Target.Transform);
        }

        public void Exit()
        {

        }
    }
}
