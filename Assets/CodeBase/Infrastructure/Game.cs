using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, ScreenFader screenFader)
        {
            StateMachine =
                new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, screenFader, AllServices.Container);
        }
    }
}