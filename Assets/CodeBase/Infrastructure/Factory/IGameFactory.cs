using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at, PlayerInputActions.PlayerActions playerInput);
        GameObject CreateHud();
        GameObject CreateKnight(GameObject at);
        GameObject CreateSpawner(EnemyStaticData data, Transform knight, LevelStaticData levelData);
        GameObject CreateGameOverUI();
        GameObject CreatePauseMenu(PlayerInputActions.PauseMenuActions pauseInput);
    }
}