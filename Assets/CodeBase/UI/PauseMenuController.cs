using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.UI
{
    public class PauseMenuController : MonoBehaviour
    {

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _settingsPanel;

        private PlayerInputActions _inputActions;
        private PauseState _state;

        public event Action Unpaused;

        public void Construct(PlayerInputActions inputActions)
        {
            _inputActions = inputActions;

            _state = PauseState.Unpaused;
        }

        private void OnEnable()
        {
            _inputActions.PauseMenu.Enable();
            _inputActions.PauseMenu.Back.performed += OnBack;
            _state = PauseState.Main;
        }

        private void OnDisable()
        {
            _inputActions.PauseMenu.Back.performed -= OnBack;
            _inputActions.PauseMenu.Disable();
        }

        public void OpenSettings()
        {
            _mainPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            _settingsPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            if (_state == PauseState.Unpaused)
            {
                Debug.LogError("Trying to back on PauseMenuController while unpaused");
                return;
            }

            _state -= 1;
            if (_state == PauseState.Unpaused)
            {
                Unpaused.Invoke();
            }
            else
            {
                CloseSettings();
            }
        }

        private enum PauseState
        {
            Unpaused,
            Main,
            Settings
        }
    }
}
