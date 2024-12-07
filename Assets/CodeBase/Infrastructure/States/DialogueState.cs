using UnityEngine.SceneManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class DialogueState : IPayloadState<string>
    {
        private const string DialogueSystemTag = "DialogueSystem";

        private GameStateMachine _stateMachine;
        private DialogueSystem _dialogueSystem;
        private readonly SceneLoader _sceneLoader;

        public DialogueState(GameStateMachine gameStateMachine, SceneLoader loader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = loader;
        }

        public void Enter(string dialogueSceneName)
        {
            _sceneLoader.Load(dialogueSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _dialogueSystem = GameObject.FindGameObjectWithTag(DialogueSystemTag).GetComponent<DialogueSystem>();
            _dialogueSystem.EndScene += OnEndScene;
        }

        private void OnEndScene(string sceneToLoad)
        {
            if (sceneToLoad == "MainMenu")
            {
                _stateMachine.Enter<MainMenuState>();
            }
            else
            {
                _stateMachine.Enter<LoadLevelState, string>(sceneToLoad);
            }
        }

        public void Exit()
        {
            _dialogueSystem.EndScene -= OnEndScene;
        }
    }
}
