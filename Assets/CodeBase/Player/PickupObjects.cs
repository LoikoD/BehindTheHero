using CodeBase.ThrowableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class PickupObjects : MonoBehaviour
    {
        [SerializeField] private Transform handsArea;
        private PlayerState _playerState;
        private HeroAnimationsController _animationController;

        private void Awake()
        {
            _animationController = GetComponentInChildren<HeroAnimationsController>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            
            if (collision.TryGetComponent<ThrowableObject>(out var throwableObject))
            {
                if (_playerState.ObjectInHands == null && throwableObject.CanBePickedUp)
                {

                    collision.transform.SetParent(handsArea, false);
                    collision.transform.localPosition = Vector3.zero;
                    _playerState.ObjectInHands = collision.gameObject;

                    _animationController.SetHasItem(true);

                    throwableObject.PickedUp();
                }
            }
        }

        public void Init(PlayerState playerState)
        {
            _playerState = playerState;
        }
    }
}