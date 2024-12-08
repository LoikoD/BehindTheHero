using System;
using System.Collections;
using CodeBase.Knight.KnightFSM;
using CodeBase.Logic;
using CodeBase.Logic.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Knight
{
    public class Knight : MonoBehaviour, IHealth
    {
        private KnightStateMachine _stateMachine;
        private KnightAnimationsController _animator;
        private HorizontalDirection _horizontalDirection;

        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }
        public Transform Transform => transform;

        public event Action HealthChanged;
        public event Action Died;

        public void Construct(KnightStateMachine stateMachine, float health)
        {
            _stateMachine = stateMachine;

            MaxHealth = health;
            CurrentHealth = MaxHealth;
            _horizontalDirection = HorizontalDirection.Right;
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<KnightAnimationsController>();
        }

        private void Update()
        {
            if (_stateMachine.HasDied)
                return;

            _stateMachine.Update();

            Turn();
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

        private void Turn()
        {
            Vector3 direction = Vector3.zero;

            if (_stateMachine.Target != null)
                direction = _stateMachine.Target.Transform.position - transform.position;

            if (direction.x > 0 && _horizontalDirection != HorizontalDirection.Right)
            {
                _horizontalDirection = HorizontalDirection.Right;
                _animator.Turn();
            }
            else if (direction.x < 0 && _horizontalDirection != HorizontalDirection.Left)
            {
                _horizontalDirection = HorizontalDirection.Left;
                _animator.Turn();
            }
        }

        private void Die()
        {
            _stateMachine.SetState<FSMStateDie>();

            float animTime = _animator.Die();

            StartCoroutine(DiedAfterTime(animTime));
        }

        private IEnumerator DiedAfterTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Died?.Invoke();
        }
    }
}