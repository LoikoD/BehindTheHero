using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "SpawnerData", menuName = "StaticData/Spawner")]
    public class SpawnerStaticData : ScriptableObject
    {
        [Header("Enemy")]
        public EnemyTypeID EnemyType;

        [Header("Spawn point")]
        public float SpawnDistanceFromEdge = 30f;
        public float MinEnemiesGap = 10f;

        [Header("Spawn pool config")]
        public int EnemiesPoolBulkAmount = 15;

        [Header("Loot")]
        public GameObject LootPool;
        public float LootSpawnChance = 0.5f;
        public int LootMaxAttemptsBeforeGuaranteedDrop = 5;
    }
}
