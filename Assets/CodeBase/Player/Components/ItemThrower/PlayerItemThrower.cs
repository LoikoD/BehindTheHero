using CodeBase.Player.Components.Animations;
using CodeBase.Player.Core.Inventory;
using UnityEngine;

namespace CodeBase.Player.Components.Thrower
{
    public class PlayerItemThrower : MonoBehaviour, IPlayerItemThrower
    {
        private IPlayerInventory _playerInventory;
        private IHeroAnimationsController _animationController;

        public void Construct(IPlayerInventory inventory, IHeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;
        }

        public void Throw(Vector2 targetPoint)
        {
            if (_playerInventory.TryDropItem(out var itemToThrow))
            {
                itemToThrow.InitThrow(targetPoint);
                _animationController.Throw();
            }
        }
    }
}
