using CodeBase.CameraLogic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuSceneName = "MainMenu";
        private const string CanvasTag = "Canvas";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ScreenFader _screenFader;

        private MainMenuController _mainMenuController;
        private CameraMover _cameraMover;
        private GameObject _canvas;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ScreenFader screenFader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _screenFader = screenFader;
        }

        public void Enter()
        {
            _screenFader.SetBlack();
            _sceneLoader.Load(MainMenuSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _canvas = GameObject.FindGameObjectWithTag(CanvasTag);

            _mainMenuController = GameObject.FindAnyObjectByType<MainMenuController>();
            _mainMenuController.StartGame += OnStartGame;

            _screenFader.ScreenFadeOut();
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
            _stateMachine.Enter<DialogueState, string>("Dialogue1");
        }

        public void Exit()
        {
            _cameraMover.Moved -= OnMoved;
            _mainMenuController.StartGame -= OnStartGame;
        }
    }
}
