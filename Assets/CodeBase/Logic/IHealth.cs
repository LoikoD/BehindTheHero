using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        public Transform Transform { get; }
        void TakeDamage(float damage);
    }
}