using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float CurrentHealth { get; set; }
        float MaxHealth { get; set; }
        public Transform Transform { get; }
        void TakeDamage(float damage);
    }
}