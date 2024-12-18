using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerItemThrower : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private HeroAnimationsController _animationController;

        public void Construct(PlayerInventory inventory, HeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;
        }

        public void Throw(Vector2 targetPoint)
        {
            if (_playerInventory.ObjectInHands != null)
            {
                IPickable objectToThrow = _playerInventory.ObjectInHands;
                objectToThrow.InitThrow(targetPoint);

                _animationController.Throw();

                _playerInventory.ObjectInHands = null;
            }

        }
    }
}
