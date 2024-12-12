using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemyStaticData> MonsterTypes;
        public int EnemiesCount;
        public int MinGroupCount;
        public int MaxGroupCount;
        public float MinSpawnDelay;
        public float MaxSpawnDelay;
    }
}