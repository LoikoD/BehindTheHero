using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at, PlayerInputActions inputActions);
        GameObject CreateHud();

        GameObject CreateKnight(GameObject at);
        //LootPiece CreateLoot();
        GameObject CreateSpawner(EnemyStaticData data, Transform knight, LevelStaticData levelData);
        GameObject CreateGameOverUI();
        GameObject CreatePauseMenu(PlayerInputActions inputActions);
    }
}