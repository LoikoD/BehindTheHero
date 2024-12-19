using CodeBase.Player.Components.Animations;
using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player.Components.Thrower
{
    public class PlayerItemThrower : MonoBehaviour, IPlayerItemThrower
    {
        private PlayerInventory _playerInventory;
        private IHeroAnimationsController _animationController;

        public void Construct(PlayerInventory inventory, IHeroAnimationsController animator)
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
