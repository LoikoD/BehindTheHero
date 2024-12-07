using CodeBase.Logic.Utilities;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private Vector2 _cursorCoords;
        private HorizontalDirection _horizontalDirection;
        private HeroAnimationsController _animationController;

        public Vector2 CurrentCoords => _cursorCoords;

        private void Awake()
        {
            _animationController = GetComponentInChildren<HeroAnimationsController>();
            _cursorCoords = Vector2.zero;

            _horizontalDirection = HorizontalDirection.Right;
        }

        public void UpdateAimCoords(Vector2 newCoords)
        {
            _cursorCoords = newCoords;

            if (_cursorCoords.x - transform.position.x > 0 && _horizontalDirection != HorizontalDirection.Right)
            {
                _horizontalDirection = HorizontalDirection.Right;
                _animationController.Turn();
            }
            else if (_cursorCoords.x - transform.position.x < 0 && _horizontalDirection != HorizontalDirection.Left)
            {
                _horizontalDirection = HorizontalDirection.Left;
                _animationController.Turn();
            }
        }
    }
}
