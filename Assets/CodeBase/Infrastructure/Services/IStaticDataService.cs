using System.Collections.Generic;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects;

namespace CodeBase.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        void LoadLevels();
        EnemyStaticData ForMonster(EnemyTypeID typeID);
        KnightStaticData ForKnight();

        LevelStaticData ForLevel(string sceneKey);
    }
}