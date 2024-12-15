﻿using UnityEngine;
using CodeBase.Dialogue;
using CodeBase.StaticData;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class DialogueState : IState
    {
        private const string DialogueSystemTag = "DialogueSystem";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ISceneService _sceneService;
        private readonly LoadingCurtain _loadingCurtain;

        private DialogueSystem _dialogueSystem;

        public DialogueState(GameStateMachine gameStateMachine, SceneLoader loader, ISceneService sceneService, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = loader;
            _sceneService = sceneService;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _sceneLoader.Load(_sceneService.CurrentScene.SceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _dialogueSystem = GameObject.FindGameObjectWithTag(DialogueSystemTag).GetComponent<DialogueSystem>();
            _dialogueSystem.Construct((DialogueStaticData)_sceneService.CurrentScene);
            _dialogueSystem.EndScene += OnEndScene;

            _loadingCurtain.Hide(StartDialogue);
        }

        private void StartDialogue()
        {
            _dialogueSystem.StartDialogue();
        }

        private void OnEndScene()
        {
            SceneStaticData nextScene = _sceneService.GetNextScene();
            if (nextScene.GetType() == typeof(LevelStaticData))
            {
                _stateMachine.Enter<LoadLevelState>();
            }
            else
            {
                _stateMachine.Enter<MainMenuState>();
            }
        }

        public void Exit()
        {
            _dialogueSystem.EndScene -= OnEndScene;
        }
    }
}
