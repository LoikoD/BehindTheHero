using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameOverUIController : MonoBehaviour
    {
        public event Action TryAgainClicked;
        public event Action MainMenuClicked;

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
