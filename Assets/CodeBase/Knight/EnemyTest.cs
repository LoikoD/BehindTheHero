using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Knight
{
    public class EnemyTest : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;
        public float Current { get; set; }
        public float Max { get; set; }
        public Transform Transform => gameObject.transform;
        public void TakeDamage(float damage)
        {
            Current -= damage;
        }
    }
}