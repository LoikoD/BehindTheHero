using CodeBase.Knight;
using CodeBase.UI;

namespace CodeBase.Infrastructure
{
    public class GameSession
    {
        public KnightMain Knight { get; private set; }
        public EnemiesSpawner EnemiesSpawner { get; private set; }
        public UIController GameUI { get; private set; }
        public PlayerInputActions InputActions { get; private set; }

        public GameSession(KnightMain knight, EnemiesSpawner enemiesSpawner, UIController gameUI, PlayerInputActions inputActions)
        {
            Knight = knight;
            EnemiesSpawner = enemiesSpawner;
            GameUI = gameUI;
            InputActions = inputActions;
        }
    }
}
