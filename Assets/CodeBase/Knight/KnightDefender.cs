using System;
using CodeBase.Knight.KnightFSM;
using CodeBase.Logic;
using CodeBase.Logic.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Knight
{
    public class KnightDefender : MonoBehaviour, IHealth
    {
        private KnightStateMachine _stateMachine;
        private KnightAnimationsController _animator;
        private HorizontalDirection _horizontalDirection;
        //[SerializeField] private SceneLoader _sceneLoader;

        public float Current { get; set; }
        public float Max { get; set; }
        public Transform Transform => transform;

        public event Action HealthChanged;

        public void Construct(KnightStateMachine stateMachine, float health)
        {
            _stateMachine = stateMachine;

            Max = health;
            Current = Max;
            _horizontalDirection = HorizontalDirection.Right;
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<KnightAnimationsController>();
        }

        private void Update()
        {
            _stateMachine.Update();

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
        
        public void TakeDamage(float damage)
        {
            Current -= damage;
            _animator.TakeDamage();
            HealthChanged?.Invoke();

            if (Current <= 0)
                Die();
        }

        private void Die()
        {
            //if (SceneManager.GetActiveScene().name == "4")
            //{
            //    _sceneLoader?.SceneChange(7);
            //}
            _animator.Die();
            Destroy(gameObject);
        }
    }
}