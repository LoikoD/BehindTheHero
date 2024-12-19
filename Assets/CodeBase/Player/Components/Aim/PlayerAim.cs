using CodeBase.Logic.Utilities;
using CodeBase.Player.Components.Animations;
using UnityEngine;

namespace CodeBase.Player.Components.Aim
{
    public class PlayerAim : MonoBehaviour, IPlayerAim
    {
        private Vector2 _cursorCoords;
        private HorizontalDirection _horizontalDirection;
        private IHeroAnimationsController _animationController;

        public Vector2 CurrentCoords => _cursorCoords;

        public void Construct(IHeroAnimationsController animator)
        {
            _animationController = animator;
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
