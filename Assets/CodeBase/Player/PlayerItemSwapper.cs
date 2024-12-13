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
            ThrowableObject backpackObject = _playerInventory.BackpackObject;

            _playerInventory.BackpackObject = _playerInventory.ObjectInHands;
            if (_playerInventory.BackpackObject != null)
            {
                _playerInventory.BackpackObject.gameObject.SetActive(false);
            }

            _playerInventory.ObjectInHands = backpackObject;
            if (_playerInventory.ObjectInHands != null)
            {
                _playerInventory.ObjectInHands.gameObject.SetActive(true);
                _animationController.SetHasItem(true);
            }
            else
            {
                _animationController.SetHasItem(false);
            }
        }
    }
}