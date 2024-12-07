using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Knight.KnightFSM
{
    public class FSMStateAttack : IFsmState
    {
        private readonly KnightStateMachine _knightStateMachine;
        private readonly KnightAttacker _attacker;
        private readonly KnightAnimationsController _animator;
        private readonly KnightStaticData _data;
        private float _distance;

        public FSMStateAttack(KnightStateMachine knightStateMachine, KnightAttacker attacker, KnightAnimationsController animator, KnightStaticData data)
        {
            _knightStateMachine = knightStateMachine;
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
            if (_knightStateMachine.Target == null)
                return;
            
            if (NeedChaseEnemy())
            {
                _knightStateMachine.SetState<FSMStateChaseEnemy>();
            }
            else
            {
                _attacker.Attack(_knightStateMachine.Target.Transform);
            }
            
            if (_knightStateMachine.Target.Transform.gameObject.activeSelf == false) 
                _knightStateMachine.SetState<FSMStateIdle>();
        }

        public void Exit()
        {
        }
        
        private bool NeedChaseEnemy()
        {
            _distance = Vector3.Distance(_attacker.gameObject.transform.position, _knightStateMachine.Target.Transform.position);
            
            if (_distance > _data.AttackRange)
                return true;

            return false;
        }
    }
}