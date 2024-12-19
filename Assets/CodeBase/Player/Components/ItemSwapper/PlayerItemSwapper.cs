using CodeBase.Player.Components.Animations;
using CodeBase.Player.Core.Inventory;
using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player.Components.ItemSwapper
{
    public class PlayerItemSwapper : MonoBehaviour, IPlayerItemSwapper
    {
        private IPlayerInventory _playerInventory;
        private IHeroAnimationsController _animationController;

        public void Construct(IPlayerInventory inventory, IHeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;

            _playerInventory.OnItemSwapped += UpdateAnimation;
        }

        private void OnDisable()
        {
            if (_playerInventory != null)
                _playerInventory.OnItemSwapped -= UpdateAnimation;
        }

        public void SwapItems()
        {
            _playerInventory.SwapItems();
        }

        private void UpdateAnimation(IPickable previous, IPickable current)
        {
            if (current != null)
            {
                current.SwapIn();
                _animationController.SetHasItem(true);
            }
            else
            {
                _animationController.SetHasItem(false);
            }
        }
    }
}