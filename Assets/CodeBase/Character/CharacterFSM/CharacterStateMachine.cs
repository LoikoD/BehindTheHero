using CodeBase.EnemiesScripts.EnemyFSM;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterStateMachine
    {
        private IFsmState _currentState;
        private readonly Dictionary<Type, IFsmState> _states = new();

        public IHealth Target { get; internal set; }
        public bool HasDied { get; internal set; }

        public void Reset()
        {
            HasDied = false;
            SetDefaultState();
        }

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

        public void SetTarget(IHealth target)
        {
            Target = target;
        }

        public void SetDieFlag()
        {
            HasDied = true;
        }

        internal abstract void SetDefaultState();
    }
}
