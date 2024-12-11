using CodeBase.Knight;

namespace CodeBase.Infrastructure
{
    public class GameSession
    {
        public KnightMain Knight { get; private set; }
        public EnemiesSpawner EnemiesSpawner { get; private set; }
        public GameUIController GameUIController { get; private set; }

        public GameSession(KnightMain knight, EnemiesSpawner enemiesSpawner, GameUIController gameUIController)
        {
            Knight = knight;
            EnemiesSpawner = enemiesSpawner;
            GameUIController = gameUIController;
        }
    }
}
