using CodeBase.ThrowableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class Backpack : MonoBehaviour
    {
        private PlayerState _playerState;

        public void SwapItems()
        {
            GameObject backpackObject = _playerState.BackpackObject;

            _playerState.BackpackObject = _playerState.ObjectInHands;
            if (_playerState.ObjectInHands != null)
            {
                _playerState.BackpackObject.SetActive(false);
            }

            _playerState.ObjectInHands = backpackObject;
            if (backpackObject != null)
            {
                _playerState.ObjectInHands.SetActive(true);
            }
        }

        public void Init(PlayerState playerState)
        {
            _playerState = playerState;
        }
    }
}