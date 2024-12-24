using CodeBase.StaticData;
using System;

namespace CodeBase.StaticData
{
    [Serializable]
    public struct LevelSpawnerData
    {
        public SpawnerStaticData SpawnerData;

        public int TotalEnemiesCount;
        public int MinGroupCount;
        public int MaxGroupCount;
        public float MinSpawnDelay;
        public float MaxSpawnDelay;
    }
}
