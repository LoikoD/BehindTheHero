using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string MenuSceneKey = "MainMenu";
        private const string DialogueSceneKey = "Dialogue";

        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _services = allServices;
            
            RegisterServices();
        }

        public void Enter()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == MenuSceneKey)
            {
                _stateMachine.Enter<MainMenuState>();
            }
            else if (currentSceneName.StartsWith(DialogueSceneKey))
            {
                _stateMachine.Enter<DialogueState, string>(currentSceneName);
            }
            else
            {
                _stateMachine.Enter<LoadLevelState, string>(currentSceneName);
            }
        }

        public void Exit()
        {
            
        }
        
        private void RegisterServices()
        {
            RegisterStaticData();
            
            _services.RegisterSingle<IAssets>(new AssetsProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            staticData.LoadLevels();
            staticData.LoadDialogues();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }
    }
}