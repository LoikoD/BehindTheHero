using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "SceneList", menuName = "StaticData/SceneList")]
    public class SceneListStaticData : ScriptableObject
    {
        public List<SceneStaticData> ScenesInOrder;
    }
}
