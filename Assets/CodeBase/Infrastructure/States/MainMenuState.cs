using CodeBase.CameraLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.SceneView;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuSceneName = "MainMenu";
        private const string CanvasTag = "Canvas";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private GameStarter _gameStarter;
        private CameraMover _cameraMover;
        private GameObject _canvas;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(MainMenuSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _canvas = GameObject.FindGameObjectWithTag(CanvasTag);

            _gameStarter = GameObject.FindAnyObjectByType<GameStarter>();
            _gameStarter.StartGame += OnStartGame;
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
            _cameraMover.Moved += OnMoved;
            _gameStarter.StartGame -= OnStartGame;
        }
    }
}
