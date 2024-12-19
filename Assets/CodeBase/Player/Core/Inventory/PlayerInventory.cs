using CodeBase.ThrowableObjects;
using System;

namespace CodeBase.Player.Core.Inventory
{
    public class PlayerInventory : IPlayerInventory
    {
        public event Action<IPickable, IPickable> OnItemSwapped;

        private IPickable _objectInHands;
        private IPickable _backpackObject;

        public bool TryPickupItem(IPickable item)
        {
            if (_objectInHands != null || !item.CanBePickedUp)
                return false;

            _objectInHands = item;
            return true;
        }

        public bool TryDropItem(out IPickable droppedItem)
        {
            droppedItem = _objectInHands;
            if (droppedItem == null)
                return false;

            _objectInHands = null;
            return true;
        }

        public void SwapItems()
        {
            _objectInHands?.SwapOut();

            (_backpackObject, _objectInHands) = (_objectInHands, _backpackObject);

            OnItemSwapped?.Invoke(_backpackObject, _objectInHands);
        }
    }
}