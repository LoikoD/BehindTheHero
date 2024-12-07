using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services;
using CodeBase.ThrowableObjects;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeID, EnemyStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private List<ThrowableObject> _loot;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<EnemyStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.Type, x => x);
        }

        public void LoadLevels()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public KnightStaticData ForKnight() => 
            Resources.Load<KnightStaticData>("StaticData/Knight/KnightData");

        public EnemyStaticData ForMonster(EnemyTypeID typeID) => 
            _monsters.TryGetValue(typeID, out EnemyStaticData data) ? data : null;
        
        public LevelStaticData ForLevel(string sceneKey) => 
            _levels.TryGetValue(sceneKey, out LevelStaticData data) ? data : null;  
    }
}