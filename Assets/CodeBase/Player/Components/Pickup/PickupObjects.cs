using CodeBase.Player.Components.Animations;
using CodeBase.Player.Core.Inventory;
using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player.Components.Pickup
{
    public class PickupObjects : MonoBehaviour
    {
        [SerializeField] private Transform handsArea;
        private IPlayerInventory _playerInventory;
        private IHeroAnimationsController _animationController;

        public void Construct(IPlayerInventory inventory, IHeroAnimationsController animator)
        {
            _playerInventory = inventory;
            _animationController = animator;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {

            if (collision.TryGetComponent<IPickable>(out var item))
            {
                if (_playerInventory.TryPickupItem(item))
                {
                    _animationController.SetHasItem(true);
                    item.PickedUp(handsArea);
                }
            }
        }
    }
}