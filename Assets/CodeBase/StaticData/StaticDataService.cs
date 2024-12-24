using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeID, EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<string, DialogueStaticData> _dialogues;

        public void LoadMonsters()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>("StaticData/Enemies")
                .ToDictionary(x => x.Type, x => x);
        }

        public void LoadLevels()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>("StaticData/ScenesData/Levels")
                .ToDictionary(x => x.SceneName, x => x);
        }

        public void LoadDialogues()
        {
            _dialogues = Resources
                .LoadAll<DialogueStaticData>("StaticData/ScenesData/Dialogue/Dialogues")
                .ToDictionary(x => x.SceneName, x => x);
        }

        public KnightStaticData ForKnight() => 
            Resources.Load<KnightStaticData>("StaticData/Knight/KnightData");

        public PlayerStaticData ForHero() =>
            Resources.Load<PlayerStaticData>("StaticData/PlayerData");

        public EnemyStaticData ForEnemy(EnemyTypeID typeID) =>
            _enemies.TryGetValue(typeID, out EnemyStaticData data) ? data : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData data) ? data : null;

        public DialogueStaticData ForDialogue(string sceneKey) =>
            _dialogues.TryGetValue(sceneKey, out DialogueStaticData data) ? data : null;

        public SceneListStaticData AllScenes() =>
            Resources.Load<SceneListStaticData>("StaticData/ScenesData/SceneList");
    }
}