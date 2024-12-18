using UnityEngine;

namespace CodeBase.Logic.Utilities
{
    public class PityDropSystem
    {
        private readonly float _decreaseAmount;
        private readonly float _increaseAmount;

        private float _currentDropChance;

        public PityDropSystem(float dropChance, int maxAttemptsBeforeGuaranteedDrop = 5)
        {
            _increaseAmount = 1f / maxAttemptsBeforeGuaranteedDrop * (1f - dropChance);
            _decreaseAmount = _increaseAmount / dropChance * (1f - dropChance);

            _currentDropChance = dropChance;
        }

        public bool ShouldDrop()
        {
            bool success = Random.value < _currentDropChance;

            if (success)
            {
                _currentDropChance -= _decreaseAmount;
            }
            else
            {
                _currentDropChance += _increaseAmount;
            }

            return success;
        }
    }
}
