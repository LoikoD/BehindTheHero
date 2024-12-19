using CodeBase.Player.Components.Animations;
using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player.Components.ItemSwapper
{
    public class PlayerItemSwapper : MonoBehaviour, IPlayerItemSwapper
    {
        private PlayerInventory _playerInventory;
        private IHeroAnimationsController _animationController;

        public void Construct(PlayerInventory inventory, IHeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;
        }

        public void SwapItems()
        {
            IPickable backpackObject = _playerInventory.BackpackObject;

            _playerInventory.ObjectInHands?.SwapOut();
            _playerInventory.BackpackObject = _playerInventory.ObjectInHands;

            _playerInventory.ObjectInHands = backpackObject;
            if (_playerInventory.ObjectInHands != null)
            {
                _playerInventory.ObjectInHands.SwapIn();
                _animationController.SetHasItem(true);
            }
            else
            {
                _animationController.SetHasItem(false);
            }
        }
    }
}