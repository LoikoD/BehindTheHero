using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerItemSwapper : MonoBehaviour
    {
        private PlayerInventory _playerInventory;

        public void Construct(PlayerInventory inventory)
        {
            _playerInventory = inventory;
        }

        public void SwapItems()
        {
            ThrowableObject backpackObject = _playerInventory.BackpackObject;

            _playerInventory.BackpackObject = _playerInventory.ObjectInHands;
            if (_playerInventory.ObjectInHands != null)
            {
                _playerInventory.BackpackObject.gameObject.SetActive(false);
            }

            _playerInventory.ObjectInHands = backpackObject;
            if (backpackObject != null)
            {
                _playerInventory.ObjectInHands.gameObject.SetActive(true);
            }
        }
    }
}