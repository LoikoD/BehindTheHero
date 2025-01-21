using UnityEngine;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        float CurrentHealth { get; set; }
        float MaxHealth { get; set; }
    }
}