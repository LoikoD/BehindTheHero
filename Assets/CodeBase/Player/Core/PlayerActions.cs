using CodeBase.Player.Components.Aim;
using CodeBase.Player.Components.ItemSwapper;
using CodeBase.Player.Components.Movement;
using CodeBase.Player.Components.Thrower;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Player.Core
{
    public class PlayerActions : MonoBehaviour
    {
        private Camera _camera;
        private IPlayerMovement _playerMovement;
        private IPlayerAim _playerAim;
        private IPlayerItemThrower _itemThrower;
        private IPlayerItemSwapper _itemSwapper;
        private PlayerInputActions.PlayerActions _playerInput;

        private Vector2 _inputVector;
    
        public void Construct(IPlayerMovement movement, IPlayerAim aim, IPlayerItemThrower itemThrower,
                              IPlayerItemSwapper itemSwapper, PlayerInputActions.PlayerActions playerInput)
        {
            _camera = Camera.main;

            _playerMovement = movement;
            _playerAim = aim;
            _itemThrower = itemThrower;
            _itemSwapper = itemSwapper;
            _playerInput = playerInput;

            _playerInput.Aim.performed += OnAim;
            _playerInput.Throw.performed += OnThrow;
            _playerInput.Swap.performed += OnSwap;
        }

        private void OnDisable()
        {
            _playerInput.Aim.performed -= OnAim;
            _playerInput.Throw.performed -= OnThrow;
            _playerInput.Swap.performed -= OnSwap;
        }

        private void Update()
        {
            _inputVector = _playerInput.Move.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            _playerMovement.Move(_inputVector);
        }

        private void OnAim(InputAction.CallbackContext context)
        {
            Vector2 newPoint = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            _playerAim.UpdateAimCoords(newPoint);
        }

        private void OnThrow(InputAction.CallbackContext context)
        {
            _itemThrower.Throw(_playerAim.CurrentCoords);
        }

        private void OnSwap(InputAction.CallbackContext context)
        {
            _itemSwapper.SwapItems();
        }
    }
}
