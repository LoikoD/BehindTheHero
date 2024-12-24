using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : SceneStaticData
    {
        public string LoadLevelText;
        public List<LevelSpawnerData> LevelSpawners;
    }

}