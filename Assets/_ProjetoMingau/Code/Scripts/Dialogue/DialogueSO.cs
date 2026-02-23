using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueSO: ScriptableObject
{
    public DialogueProfileSO Speaker;
    [TextArea] public string DialogueText;
    public Sprite CustomEmotion;
}