using UnityEngine;

namespace CodeBase.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueCharacter", menuName = "StaticData/DialogueCharacter")]
    public class DialogueCharacterStaticData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
    }
}
