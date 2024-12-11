using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;

        public event Action TryAgainClicked;
        public event Action MainMenuClicked;

        private void Awake()
        {
            HideGameOverPanel();
        }

        public void ShowGameOverPanel()
        {
            _gameOverPanel.SetActive(true);
        }

        public void HideGameOverPanel()
        {
            _gameOverPanel.SetActive(false);
        }

        public void TryAgainClick()
        {
            TryAgainClicked.Invoke();
        }

        public void MainMenuClick()
        {
            MainMenuClicked.Invoke();
        }
        
    }
}
