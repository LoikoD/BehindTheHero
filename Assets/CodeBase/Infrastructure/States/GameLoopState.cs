using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IPayloadState<GameObject>
    {
        private GameStateMachine _stateMachine;
        private EnemiesSpawner _spawner;
        private readonly SceneLoader _sceneLoader;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader loader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = loader;
        }

        public void Enter(GameObject payload)
        {
            _spawner = payload.GetComponent<EnemiesSpawner>();

            _spawner.EndLevel += OnEndLevel;
        }

        private void OnEndLevel()
        {
            _sceneLoader.Load($"Dialogue{SceneManager.GetActiveScene().name}");
        }

        public void Exit()
        {
            _spawner.EndLevel -= OnEndLevel;
        }
    }
}