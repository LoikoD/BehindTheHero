using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.Player
{
    public class ThrowAction : MonoBehaviour
    {
        [SerializeField] private Transform _objectsTransform;
        private HeroAnimationsController _animationController;
        private PlayerState _playerState;

        public void Init(PlayerState playerState)
        {
            _playerState = playerState;
            _animationController = GetComponentInChildren<HeroAnimationsController>();
        }

        public void Throw(Vector2 targetPoint)
        {
            if (_playerState.ObjectInHands != null)
            {
                GameObject objectToThrow = _playerState.ObjectInHands;
                objectToThrow.transform.SetParent(_objectsTransform, true);
                objectToThrow.GetComponent<ThrowableObject>().InitThrow(targetPoint);

                _animationController.Throw();

                _playerState.ObjectInHands = null;
            }

        }
    }
}
