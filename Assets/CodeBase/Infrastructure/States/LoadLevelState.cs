using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Knight;
using CodeBase.StaticData;
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
        private LoadingCurtain _loadingCurtain;

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
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            GameObject spawner = InitGameWorld();

            _stateMachine.Enter<GameLoopState, GameObject>(spawner);
        }

        private GameObject InitGameWorld()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(HeroSpawnTag));
            GameObject knight = _gameFactory.CreateKnight(GameObject.FindGameObjectWithTag(KnightSpawnTag));
            
            InitHud(knight);
            CameraFollow(knight);
            return InitSpawners(knight);
        }

        private GameObject InitSpawners(GameObject knight)
        {
            GameObject spawner = null;
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            
            foreach (EnemyStaticData enemyData in levelData.MonsterTypes)
            {
                spawner = _gameFactory.CreateSpawner(enemyData, knight.transform);
            }

            return spawner;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            hud.GetComponent<PlayerUI>().Construct(hero.GetComponent<KnightDefender>());
        }

        private void CameraFollow(GameObject gameObject) => 
            Camera.main.gameObject.GetComponent<CameraFollow>().Follow(gameObject);
    }
}