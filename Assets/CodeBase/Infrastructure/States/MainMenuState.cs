using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string CanvasTag = "Canvas";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneService _sceneService;

        private MainMenuController _mainMenuController;
        private CameraMover _cameraMover;
        private GameObject _canvas;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ISceneService sceneService, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _sceneService = sceneService;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show(() => _sceneLoader.Load(_sceneService.CurrentScene.SceneName, OnLoaded));
            
        }

        private void OnLoaded()
        {
            _canvas = GameObject.FindGameObjectWithTag(CanvasTag);

            _mainMenuController = GameObject.FindAnyObjectByType<MainMenuController>();
            _mainMenuController.StartGame += OnStartGame;

            _loadingCurtain.Hide(() => { });
        }

        private void OnStartGame()
        {
            _cameraMover = Camera.main.GetComponent<CameraMover>();
            _canvas.SetActive(false);
            _cameraMover.StartMoving();
            _cameraMover.Moved += OnMoved;
        }

        private void OnMoved()
        {
            _sceneService.GetNextScene();
            _stateMachine.Enter<DialogueState>();
        }

        public void Exit()
        {
            _cameraMover.Moved -= OnMoved;
            _mainMenuController.StartGame -= OnStartGame;
        }
    }
}
