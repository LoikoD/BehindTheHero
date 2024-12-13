using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        void LoadLevels();
        EnemyStaticData ForMonster(EnemyTypeID typeID);
        KnightStaticData ForKnight();
        PlayerStaticData ForHero();
        LevelStaticData ForLevel(string sceneKey);
    }
}