using CodeBase.Player.Components.Animations;
using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player.Components.Pickup
{
    public class PickupObjects : MonoBehaviour
    {
        [SerializeField] private Transform handsArea;
        private PlayerInventory _playerInventory;
        private IHeroAnimationsController _animationController;

        public void Construct(PlayerInventory inventory, IHeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {

            if (collision.TryGetComponent<IPickable>(out var pickup))
            {
                if (_playerInventory.ObjectInHands == null && pickup.CanBePickedUp)
                {
                    _playerInventory.ObjectInHands = pickup;

                    _animationController.SetHasItem(true);

                    pickup.PickedUp(handsArea);
                }
            }
        }
    }
}