using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public Dialogue LoadDialogue(string jsonText)
    {
        Dialogue _dialogue = JsonUtility.FromJson<Dialogue>(jsonText);
        return _dialogue;
    }
}
