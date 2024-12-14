using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Player
{
    public class PlayerActions : MonoBehaviour
    {
        private Camera _camera;
        private PlayerMovement _playerMovement;
        private PlayerAim _playerAim;
        private PlayerItemThrower _itemThrower;
        private PlayerItemSwapper _itemSwapper;
        private PlayerInputActions _playerInputActions;

        private Vector2 _inputVector;
    
        public void Construct(PlayerMovement movement, PlayerAim aim, PlayerItemThrower itemThrower,
                              PlayerItemSwapper itemSwapper, PlayerInputActions inputActions)
        {
            _camera = Camera.main;

            _playerMovement = movement;
            _playerAim = aim;
            _itemThrower = itemThrower;
            _itemSwapper = itemSwapper;
            _playerInputActions = inputActions;

            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Aim.performed += OnAim;
            _playerInputActions.Player.Throw.performed += OnThrow;
            _playerInputActions.Player.Swap.performed += OnSwap;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Aim.performed -= OnAim;
            _playerInputActions.Player.Throw.performed -= OnThrow;
            _playerInputActions.Player.Swap.performed -= OnSwap;
            _playerInputActions.Player.Disable();
        }

        private void Update()
        {
            _inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            _playerMovement.Move(_inputVector);
        }

        public void EnableControls()
        {
            _playerInputActions.Player.Enable();
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
