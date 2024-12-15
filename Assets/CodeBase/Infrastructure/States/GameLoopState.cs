using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IPayloadState<GameSession>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneService _sceneService;
        private readonly LoadingCurtain _loadingCurtain;

        private GameSession _gameSession;

        public GameLoopState(GameStateMachine gameStateMachine, ISceneService sceneService, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneService = sceneService;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(GameSession gameSession)
        {
            _gameSession = gameSession;

            _gameSession.EnemiesSpawner.EndLevel += OnEndLevel;
            _gameSession.Knight.Died += OnDied;

            _gameSession.GameUI.GameOverUI.TryAgainClicked += OnTryAgain;
            _gameSession.GameUI.GameOverUI.MainMenuClicked += OnMainMenu;

            _gameSession.InputActions.Player.Pause.performed += OnPause;
            _gameSession.GameUI.PauseMenu.Unpaused += OnUnpause;
            _loadingCurtain.Hide(StartGame);
        }

        private void StartGame()
        {
            _gameSession.InputActions.Player.Enable();
            _gameSession.EnemiesSpawner.StartSpawning();
        }

        private void OnEndLevel()
        {
            _sceneService.GetNextScene();

            _loadingCurtain.Show(() => _stateMachine.Enter<DialogueState>());
        }

        private void OnDied()
        {
            Time.timeScale = 0;
            _gameSession.GameUI.GameOverUI.gameObject.SetActive(true);
        }

        private void OnTryAgain()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<LoadLevelState>();
        }

        private void OnMainMenu()
        {
            Time.timeScale = 1;
            _sceneService.SetFirstScene();
            _stateMachine.Enter<MainMenuState>();
        }

        private void OnPause(InputAction.CallbackContext _)
        {
            Time.timeScale = 0;
            _gameSession.InputActions.Player.Disable();
            _gameSession.GameUI.PauseMenu.gameObject.SetActive(true);
        }

        private void OnUnpause()
        {
            Time.timeScale = 1;
            _gameSession.GameUI.PauseMenu.gameObject.SetActive(false);
            _gameSession.InputActions.Player.Enable();
        }

        public void Exit()
        {
            _gameSession.EnemiesSpawner.EndLevel -= OnEndLevel;
            _gameSession.Knight.Died -= OnDied;
            _gameSession.GameUI.GameOverUI.TryAgainClicked -= OnTryAgain;
            _gameSession.GameUI.GameOverUI.MainMenuClicked -= OnMainMenu;
            _gameSession.InputActions.Player.Pause.performed -= OnPause;
            _gameSession.GameUI.PauseMenu.Unpaused -= OnUnpause;
        }
    }
}