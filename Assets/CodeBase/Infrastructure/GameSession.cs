using CodeBase.Knight;
using CodeBase.Player;
using CodeBase.UI;

namespace CodeBase.Infrastructure
{
    public class GameSession
    {
        public KnightMain Knight { get; private set; }
        public EnemiesSpawner EnemiesSpawner { get; private set; }
        public UIController GameUI { get; private set; }
        public PlayerActions PlayerActions { get; private set; }

        public GameSession(KnightMain knight, EnemiesSpawner enemiesSpawner, UIController gameUI, PlayerActions playerActions)
        {
            Knight = knight;
            EnemiesSpawner = enemiesSpawner;
            GameUI = gameUI;
            PlayerActions = playerActions;
        }
    }
}
