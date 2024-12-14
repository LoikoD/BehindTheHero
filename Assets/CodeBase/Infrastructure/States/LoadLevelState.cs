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
        private const string ObjectsHolderTag = "ObjectsHolder";

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
            _loadingCurtain.Show();

            _sceneLoader.Load(_sceneService.CurrentScene.SceneName, OnLoaded, true);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            GameSession gameSession = InitGameWorld();

            _stateMachine.Enter<GameLoopState, GameSession>(gameSession);
        }

        private GameSession InitGameWorld()
        {
            PlayerInputActions inputActions = new();

            _gameFactory.CreateHero(
                GameObject.FindGameObjectWithTag(HeroSpawnTag),
                GameObject.FindGameObjectWithTag(ObjectsHolderTag).transform,
                inputActions);

            GameObject knight = _gameFactory.CreateKnight(GameObject.FindGameObjectWithTag(KnightSpawnTag));

            KnightMain knightMain = knight.GetComponent<KnightMain>();
            EnemiesSpawner enemiesSpawner = InitSpawners(knight).GetComponent<EnemiesSpawner>();

            InitHud(knightMain);
            CameraFollow(knight);

            UIController gameUI = InitUI(inputActions);
            GameSession gameSession = new(knightMain, enemiesSpawner, gameUI, inputActions);

            return gameSession;
        }

        private UIController InitUI(PlayerInputActions inputActions)
        {
            PauseMenuController pauseMenu = _gameFactory.CreatePauseMenu(inputActions).GetComponent<PauseMenuController>();
            GameOverUIController gameOverUI = _gameFactory.CreateGameOverUI().GetComponent<GameOverUIController>();

            UIController uiController = new(pauseMenu, gameOverUI);

            return uiController;
        }

        private GameObject InitSpawners(GameObject knight)
        {
            GameObject spawner = null;

            LevelStaticData levelStaticData = (LevelStaticData)_sceneService.CurrentScene;
            
            foreach (EnemyStaticData enemyData in levelStaticData.MonsterTypes)
            {
                spawner = _gameFactory.CreateSpawner(enemyData, knight.transform, levelStaticData);
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