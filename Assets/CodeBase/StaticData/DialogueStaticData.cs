using CodeBase.Dialogue;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "StaticData/Dialogue")]
    public class DialogueStaticData : ScriptableObject
    {
        public string SceneName;
        public List<DialogueBlock> Blocks;
        public string NextLevelName;
        public int FlashBackStart;
        public int FlashBackEnd;
    }
}
