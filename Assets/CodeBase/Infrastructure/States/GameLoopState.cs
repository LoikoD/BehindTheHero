using CodeBase.Infrastructure.Services;
using CodeBase.Knight;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IPayloadState<GameSession>
    {
        private readonly GameStateMachine _stateMachine;
        private GameSession _gameSession;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public void Enter(GameSession gameSession)
        {
            _gameSession = gameSession;

            _gameSession.EnemiesSpawner.EndLevel += OnEndLevel;
            _gameSession.Knight.Died += OnDied;

            _gameSession.GameUI.GameOverUI.TryAgainClicked += OnTryAgain;
            _gameSession.GameUI.GameOverUI.MainMenuClicked += OnMainMenu;

            _gameSession.PlayerActions.Paused += OnPause;
            _gameSession.GameUI.PauseMenu.Unpaused += OnUnpause;
        }

        private void OnEndLevel()
        {
            _stateMachine.Enter<DialogueState, string>($"Dialogue{SceneManager.GetActiveScene().name}");
        }

        private void OnDied()
        {
            Time.timeScale = 0;
            _gameSession.GameUI.GameOverUI.gameObject.SetActive(true);
        }

        private void OnTryAgain()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
        }

        private void OnMainMenu()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<MainMenuState>();
        }

        private void OnPause()
        {
            Time.timeScale = 0;
            _gameSession.GameUI.PauseMenu.gameObject.SetActive(true);
        }

        private void OnUnpause()
        {
            Time.timeScale = 1;
            _gameSession.GameUI.PauseMenu.gameObject.SetActive(false);
            _gameSession.PlayerActions.EnableControls();
        }

        public void Exit()
        {
            _gameSession.EnemiesSpawner.EndLevel -= OnEndLevel;
            _gameSession.Knight.Died -= OnDied;
            _gameSession.GameUI.GameOverUI.TryAgainClicked -= OnTryAgain;
            _gameSession.GameUI.GameOverUI.MainMenuClicked -= OnMainMenu;
            _gameSession.PlayerActions.Paused -= OnPause;
            _gameSession.GameUI.PauseMenu.Unpaused -= OnUnpause;
        }
    }
}