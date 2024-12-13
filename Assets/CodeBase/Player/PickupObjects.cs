using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player
{
    public class PickupObjects : MonoBehaviour
    {
        [SerializeField] private Transform handsArea;
        private PlayerInventory _playerInventory;
        private HeroAnimationsController _animationController;

        public void Construct(PlayerInventory inventory, HeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            
            if (collision.TryGetComponent<ThrowableObject>(out var throwableObject))
            {
                if (_playerInventory.ObjectInHands == null && throwableObject.CanBePickedUp)
                {

                    collision.transform.SetParent(handsArea, false);
                    collision.transform.localPosition = Vector3.zero;
                    _playerInventory.ObjectInHands = throwableObject;

                    _animationController.SetHasItem(true);

                    throwableObject.PickedUp();
                }
            }
        }
    }
}