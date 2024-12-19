using CodeBase.ThrowableObjects;
using System;

namespace CodeBase.Player.Core.Inventory
{
    public interface IPlayerInventory
    {
        event Action<IPickable, IPickable> OnItemSwapped;

        void SwapItems();
        bool TryDropItem(out IPickable droppedItem);
        bool TryPickupItem(IPickable item);
    }
}