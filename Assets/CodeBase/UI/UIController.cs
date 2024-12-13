using CodeBase.Infrastructure;
using CodeBase.Knight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.UI
{
    public class UIController
    {
        public PauseMenuController PauseMenu { get; private set; }
        public GameOverUIController GameOverUI { get; private set; }

        public UIController(PauseMenuController pauseMenu, GameOverUIController gameOverUI)
        {
            PauseMenu = pauseMenu;
            GameOverUI = gameOverUI;
        }
    }
}
