using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Knight.KnightFSM
{
    public class KnightStateMachine
    {
        private IFsmState _currentState;
        private Dictionary<Type, IFsmState> _states = new Dictionary<Type, IFsmState>();

        public KnightStateMachine(KnightMover movement, KnightAttacker attacker, KnightStaticData data, KnightAnimationsController animator)
        {
            AddState(new FSMStateIdle(this, movement, data, animator));
            AddState(new FSMStateChaseEnemy(this, movement, animator, data));
            AddState(new FSMStateAttack(this, attacker, animator, data));
            
            SetState<FSMStateIdle>();
        }

        public IHealth Target { get; private set; }

        public void AddState(IFsmState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void SetState<T>() where T : IFsmState
        {
            var type = typeof(T);

            if (_currentState?.GetType() != type)
            {
                if (_states.TryGetValue(type, out var newState))
                {
                    _currentState?.Exit();

                    _currentState = newState;

                    _currentState.Enter();
                }
            }
        }
        
        public void SetTarget(IHealth target)
        {
            Target = target;
        }
    }
}