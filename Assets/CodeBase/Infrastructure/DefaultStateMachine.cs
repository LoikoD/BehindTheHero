using CodeBase.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public abstract class DefaultStateMachine
    {
        private protected IFsmState _currentState;
        private protected readonly Dictionary<Type, IFsmState> _states = new();

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
            var targetType = typeof(T);

            if (_currentState?.GetType() != targetType)
            {
                var matchingState = _states.FirstOrDefault(pair => targetType.IsAssignableFrom(pair.Key)).Value;

                if (matchingState != null)
                {
                    _currentState?.Exit();
                    _currentState = matchingState;
                    _currentState.Enter();
                }
                else
                {
                    Debug.LogError($"State of type {targetType} not found.");
                }
            }
        }
    }
}
