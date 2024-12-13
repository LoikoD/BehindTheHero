using UnityEngine;
using CodeBase.Dialogue;
using CodeBase.StaticData;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class DialogueState : IPayloadState<string>
    {
        private const string DialogueSystemTag = "DialogueSystem";

        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticData;

        private string _sceneKey;
        private DialogueSystem _dialogueSystem;
        private readonly SceneLoader _sceneLoader;

        public DialogueState(GameStateMachine gameStateMachine, SceneLoader loader, IStaticDataService staticData)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = loader;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _sceneKey = sceneName;
            _sceneLoader.Load(_sceneKey, OnLoaded);
        }

        private void OnLoaded()
        {
            DialogueStaticData data = _staticData.ForDialogue(_sceneKey);

            _dialogueSystem = GameObject.FindGameObjectWithTag(DialogueSystemTag).GetComponent<DialogueSystem>();
            _dialogueSystem.Construct(data);
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
