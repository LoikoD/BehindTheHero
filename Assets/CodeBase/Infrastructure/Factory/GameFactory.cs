using CodeBase.Character;
using CodeBase.EnemiesScripts.EnemyFSM;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services;
using CodeBase.Knight;
using CodeBase.Knight.KnightFSM;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        public GameObject CreateHero(GameObject at)
        {
            GameObject hero = _assets.InstantiateAt(AssetPath.Hero, at.transform.position);
            
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
        

        public GameObject CreateSpawner(EnemyStaticData enemyType, Transform knight)
        {
            var prefab = Resources.Load<GameObject>(AssetPath.Spawner);
            GameObject spawner = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            
            spawner.GetComponent<EnemiesSpawner>().Construct(knight, enemyType);

            return spawner;
        }

        public GameObject CreateGameUI()
        {
            return _assets.Instantiate(AssetPath.GameUI);
        }
    }
}