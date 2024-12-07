using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuSceneName = "MainMenu";

        private readonly GameStateMachine _stateMachine;
        private GameStarter _gameStarter;
        private readonly SceneLoader _sceneLoader;

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
            _gameStarter = GameObject.FindAnyObjectByType<GameStarter>();
            _gameStarter.StartGame += OnStartGame;
        }

        private void OnStartGame()
        {
            _stateMachine.Enter<DialogueState, string>("Dialogue1");
        }

        public void Exit()
        {
            _gameStarter.StartGame -= OnStartGame;
        }
    }
}
