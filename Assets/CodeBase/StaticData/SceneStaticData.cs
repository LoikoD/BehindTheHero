using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "StaticData/Scene")]
    public class SceneStaticData : ScriptableObject
    {
        public string SceneName;
    }
}
