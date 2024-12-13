using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerItemThrower : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private HeroAnimationsController _animationController;
        private Transform _objectsTransform;

        public void Construct(PlayerInventory inventory, HeroAnimationsController animator, Transform objectsHolder)
        {
            _playerInventory = inventory;
            _animationController = animator;
            _objectsTransform = objectsHolder;
        }

        public void Throw(Vector2 targetPoint)
        {
            if (_playerInventory.ObjectInHands != null)
            {
                ThrowableObject objectToThrow = _playerInventory.ObjectInHands;
                objectToThrow.transform.SetParent(_objectsTransform, true);
                objectToThrow.InitThrow(targetPoint);

                _animationController.Throw();

                _playerInventory.ObjectInHands = null;
            }

        }
    }
}
