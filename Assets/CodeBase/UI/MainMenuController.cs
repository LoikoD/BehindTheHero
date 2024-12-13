using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Image _settingsPanel;

        private PanelOpener _panelOpener;

        public event Action StartGame;

        private void Start()
        {
            _panelOpener = new();
        }

        public void Play()
        {
            StartGame?.Invoke();
        }

        public void Settings()
        {
            _panelOpener.PanelClck(_settingsPanel);
        }
    }
}