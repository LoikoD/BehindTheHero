using CodeBase.Character;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services;
using CodeBase.Knight;
using CodeBase.Knight.KnightFSM;
using CodeBase.Player;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using CodeBase.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    internal class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssets assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHero(GameObject at, Transform objectsHolder, PlayerInputActions inputActions)
        {
            GameObject hero = _assets.InstantiateAt(AssetPath.Hero, at.transform.position);

            HeroAnimationsController animator = hero.GetComponent<HeroAnimationsController>();

            PlayerInventory inventory = new();

            PlayerMovement playerMovement = hero.GetComponent<PlayerMovement>();
            PlayerStaticData playerData = _staticData.ForHero();
            playerMovement.Construct(animator, playerData);

            PlayerAim playerAim = hero.GetComponent<PlayerAim>();
            playerAim.Construct(animator);

            PlayerItemThrower itemThrower = hero.GetComponent<PlayerItemThrower>();
            itemThrower.Construct(inventory, animator, objectsHolder);

            PlayerItemSwapper itemSwapper = hero.GetComponent<PlayerItemSwapper>();
            itemSwapper.Construct(inventory);

            hero.GetComponent<PickupObjects>().Construct(inventory, animator);

            PlayerActions playerActions = hero.GetComponent<PlayerActions>();
            playerActions.Construct(playerMovement, playerAim, itemThrower, itemSwapper, inputActions);

            return hero;
        }

        public GameObject CreateKnight(GameObject at)
        {
            KnightStaticData knightData = _staticData.ForKnight();
            GameObject knight = _assets.InstantiateAt(AssetPath.Knight, at.transform.position);

            KnightSounds sounds = knight.GetComponent<KnightSounds>();
            sounds.Construct();

            KnightAnimationsController animator = knight.GetComponent<KnightAnimationsController>();
            animator.Construct(sounds);

            KnightMover mover = knight.GetComponent<KnightMover>();
            mover.Construct(knightData.MoveSpeed);

            KnightAttacker attacker = knight.GetComponent<KnightAttacker>();
            List<Weapon> weapons = knight.GetComponentsInChildren<Weapon>(true).ToList();
            attacker.Construct(animator, weapons);

            KnightStateMachine knightStateMachine = new(mover, attacker, knightData, animator);

            knight.GetComponentInChildren<KnightPickupObjects>().Construct(knightStateMachine, attacker, knightData.PickUpRange);
            knight.GetComponent<KnightMain>().Construct(knightStateMachine, animator, knightData.Hp);
            knight.GetComponent<CharacterTurner>().Construct(knightStateMachine, animator);

            return knight;
        }

        public GameObject CreateHud() => 
            _assets.Instantiate(AssetPath.Hud);
        

        public GameObject CreateSpawner(EnemyStaticData enemyType, Transform knight, LevelStaticData levelData)
        {
            var prefab = Resources.Load<GameObject>(AssetPath.Spawner);
            GameObject spawner = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            
            spawner.GetComponent<EnemiesSpawner>().Construct(knight, enemyType, levelData);

            return spawner;
        }

        public GameObject CreateGameOverUI()
        {
            return _assets.Instantiate(AssetPath.GameOverUI);
        }

        public GameObject CreatePauseMenu(PlayerInputActions inputActions)
        {
            GameObject pauseMenu = _assets.Instantiate(AssetPath.PauseMenu);
            pauseMenu.GetComponent<PauseMenuController>().Construct(inputActions);

            return pauseMenu;
        }
    }
}