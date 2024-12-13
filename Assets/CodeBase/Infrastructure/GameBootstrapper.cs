using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private ScreenFader _screenFaderPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab), Instantiate(_screenFaderPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}