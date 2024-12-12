using CodeBase.Character.Interfaces;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Logic.Utilities;
using CodeBase.StaticData;

namespace CodeBase.Knight.KnightFSM
{
    public class KnightFSMIdleState : IFsmState
    {
        private readonly KnightStateMachine _stateMachine;
        private readonly KnightMover _movement;
        private readonly ClosestTargetFinder _targetFinder;
        private readonly IAnimationsController _animator;

        public KnightFSMIdleState(KnightStateMachine stateMachine, KnightMover movement, KnightStaticData data, KnightAnimationsController animator)
        {
            _stateMachine = stateMachine;
            _movement = movement;
            _targetFinder = new ClosestTargetFinder(data.AggroRange, data.TargetLayer);
            _animator = animator;
        }

        public void Enter()
        {
            _animator.Idle();
        }

        public void Update()
        {
            if (_targetFinder.TryFindTarget(_movement.gameObject.transform.position, out IHealth target))
            {
                _stateMachine.SetTarget(target);
                _stateMachine.SetState<KnightFSMChaseState>();
            }
        }

        public void Exit()
        {
        }
    }
}