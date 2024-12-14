using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
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

            ISceneService sceneService = _services.Single<ISceneService>();
            sceneService.SetCurrentScene(currentSceneName);

            if (sceneService.CurrentScene.GetType() == typeof(DialogueStaticData))
            {
                _stateMachine.Enter<DialogueState>();
            }
            else if (sceneService.CurrentScene.GetType() == typeof(LevelStaticData))
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
            
        }
        
        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IAssets>(new AssetsProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>()));
            _services.RegisterSingle<ISceneService>(new SceneService(_services.Single<IStaticDataService>()));
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