using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Knight;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string HeroSpawnTag = "HeroSpawn";
        private const string KnightSpawnTag = "KnightSpawn";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ISceneService _sceneService;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            ISceneService sceneService)
        {
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _sceneService = sceneService;
        }

        public void Enter()
        {
            string loadText = (_sceneService.CurrentScene as LevelStaticData).LoadLevelText;
            _loadingCurtain.Show(
                () => _sceneLoader.Load(_sceneService.CurrentScene.SceneName, OnLoaded, true),
                loadText);
        }

        public void Exit()
        {

        }

        private void OnLoaded()
        {
            GameSession gameSession = InitGameWorld();

            _stateMachine.Enter<GameLoopState, GameSession>(gameSession);
        }

        private GameSession InitGameWorld()
        {
            PlayerInputActions inputActions = new();

            _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(HeroSpawnTag), inputActions.Player);

            GameObject knight = _gameFactory.CreateKnight(GameObject.FindGameObjectWithTag(KnightSpawnTag));

            KnightMain knightMain = knight.GetComponent<KnightMain>();
            EnemiesSpawner enemiesSpawner = InitSpawners(knight).GetComponent<EnemiesSpawner>();

            InitHud(knightMain);
            CameraFollow(knight);

            UIController gameUI = InitUI(inputActions.PauseMenu);
            GameSession gameSession = new(knightMain, enemiesSpawner, gameUI, inputActions);

            return gameSession;
        }

        private UIController InitUI(PlayerInputActions.PauseMenuActions pauseInput)
        {
            PauseMenuController pauseMenu = _gameFactory.CreatePauseMenu(pauseInput).GetComponent<PauseMenuController>();
            GameOverUIController gameOverUI = _gameFactory.CreateGameOverUI().GetComponent<GameOverUIController>();

            UIController uiController = new(pauseMenu, gameOverUI);

            return uiController;
        }

        private GameObject InitSpawners(GameObject knight)
        {
            GameObject spawner = null;

            LevelStaticData levelStaticData = (LevelStaticData)_sceneService.CurrentScene;
            
            foreach (LevelSpawnerData levelSpawnerData in levelStaticData.LevelSpawners)
            {
                spawner = _gameFactory.CreateSpawner(knight.transform, levelSpawnerData);
            }

            return spawner;
        }

        private void InitHud(KnightMain knightMain)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            hud.GetComponent<HudUI>().Construct(knightMain);
        }

        private void CameraFollow(GameObject gameObject) => 
            Camera.main.gameObject.GetComponent<CameraFollow>().Follow(gameObject);
    }
}