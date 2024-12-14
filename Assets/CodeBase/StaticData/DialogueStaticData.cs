using CodeBase.Dialogue;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "StaticData/Dialogue")]
    public class DialogueStaticData : SceneStaticData
    {
        public List<DialogueBlock> Blocks;
        public int FlashBackStart;
        public int FlashBackEnd;
    }
}
