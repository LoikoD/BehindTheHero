using CodeBase.Logic;
using System;
using System.Collections;
using UnityEngine;
using CodeBase.Character.CharacterFSM;
using CodeBase.Character.Interfaces;

namespace CodeBase.Character
{
    public abstract class CharacterMain : MonoBehaviour, IHealth
    {
        private CharacterStateMachine _stateMachine;
        private IAnimationsController _animator;

        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }
        public Transform Transform => transform;

        public event Action HealthChanged;

        public void Construct(CharacterStateMachine stateMachine, IAnimationsController animator, float health)
        {
            _stateMachine = stateMachine;
            _animator = animator;

            MaxHealth = health;
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
            if (_stateMachine.HasDied)
                return;

            _stateMachine.Update();
        }

        public void TakeDamage(float damage)
        {
            if (_stateMachine.HasDied)
                return;

            CurrentHealth -= damage;
            _animator.TakeDamage();
            HealthChanged?.Invoke();

            if (CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _stateMachine.SetState<CharacterFSMDieState>();

            float animTime = _animator.Die();

            StartCoroutine(DeadAfterTime(animTime));
        }

        private IEnumerator DeadAfterTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Dead();
        }

        internal abstract void Dead();
    }
}
