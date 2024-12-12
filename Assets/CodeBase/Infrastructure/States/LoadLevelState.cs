using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Knight;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string HeroSpawnTag = "HeroSpawn";
        private const string KnightSpawnTag = "KnightSpawn";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IStaticDataService staticData)
        {
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded, true);
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
            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(HeroSpawnTag));
            GameObject knight = _gameFactory.CreateKnight(GameObject.FindGameObjectWithTag(KnightSpawnTag));

            KnightMain knightMain = knight.GetComponent<KnightMain>();
            EnemiesSpawner enemiesSpawner = InitSpawners(knight).GetComponent<EnemiesSpawner>();
            GameObject gameUI = _gameFactory.CreateGameUI();

            InitHud(knightMain);
            CameraFollow(knight);


            GameSession gameSession = new(knightMain, enemiesSpawner, gameUI.GetComponent<GameUIController>());

            return gameSession;
        }

        private GameObject InitSpawners(GameObject knight)
        {
            GameObject spawner = null;
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            
            foreach (EnemyStaticData enemyData in levelData.MonsterTypes)
            {
                spawner = _gameFactory.CreateSpawner(enemyData, knight.transform, levelData);
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