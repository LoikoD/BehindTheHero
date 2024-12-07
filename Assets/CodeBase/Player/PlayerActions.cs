using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Player
{
    public class PlayerActions : MonoBehaviour
    {
        public GameObject _objectToThrow; // temp

        private PlayerInputActions _playerInputActions;
        private Camera _camera;

        private PlayerMovement _playerMovement;
        private PlayerAim _playerAim;
        private ThrowAction _throwAction;
        private Backpack _backpack;

        private PlayerState _playerState;

        private GameObject _menuPanel;

        // Input
        private Vector2 _inputVector;
    
        private void Awake()
        {
            // Input Action
            _playerInputActions = new PlayerInputActions();

            // Camera
            _camera = Camera.main;

            // Player State
            _playerState = new PlayerState();

            // Actions
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAim = GetComponent<PlayerAim>();
            _throwAction = GetComponent<ThrowAction>();
            _throwAction.Init(_playerState);
            _backpack = GetComponent<Backpack>();
            _backpack.Init(_playerState);
            GetComponent<PickupObjects>().Init(_playerState);

            _menuPanel = GameObject.FindGameObjectWithTag("PauseMenu");
            _menuPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Aim.performed += OnAim;
            _playerInputActions.Player.Throw.performed += OnThrow;
            _playerInputActions.Player.Swap.performed += OnSwap;
            _playerInputActions.Player.Menu.performed += OnMenu;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Aim.performed -= OnAim;
            _playerInputActions.Player.Throw.performed -= OnThrow;
            _playerInputActions.Player.Swap.performed -= OnSwap;
            _playerInputActions.Player.Menu.performed -= OnMenu;
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

        private void OnAim(InputAction.CallbackContext context)
        {
            Vector2 newPoint = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            _playerAim.UpdateAimCoords(newPoint);
        }

        private void OnThrow(InputAction.CallbackContext context)
        {
            _throwAction.Throw(_playerAim.CurrentCoords);
        }

        private void OnSwap(InputAction.CallbackContext context)
        {
            _backpack.SwapItems();
        }

        private void OnMenu(InputAction.CallbackContext context)
        {
            Time.timeScale = 0;
            _menuPanel.SetActive(true);
            _playerInputActions.Player.Disable();
            _playerInputActions.PauseMenu.Enable();
            _playerInputActions.PauseMenu.Continue.performed += OnContinue;
        }

        private void OnContinue(InputAction.CallbackContext context)
        {
            Time.timeScale = 1;
            _menuPanel.SetActive(false);
            _playerInputActions.PauseMenu.Continue.performed -= OnContinue;
            _playerInputActions.PauseMenu.Disable();
            _playerInputActions.Player.Enable();
        }
    }
}
