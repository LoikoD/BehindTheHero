using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Image _settingsPanel;
        [SerializeField] private UISoundController _soundController;

        private PanelOpener _panelOpener;

        public event Action StartGame;

        private void Start()
        {
            _panelOpener = new();
        }

        public void Play()
        {
            _soundController.PlayButtonClickSound();
            StartGame?.Invoke();
        }

        public void Settings()
        {
            _soundController.PlayButtonClickSound();
            _panelOpener.PanelClck(_settingsPanel);
        }
    }
}