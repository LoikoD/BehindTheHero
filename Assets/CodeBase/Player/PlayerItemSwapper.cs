using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerItemSwapper : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private HeroAnimationsController _animationController;

        public void Construct(PlayerInventory inventory, HeroAnimationsController animator)
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