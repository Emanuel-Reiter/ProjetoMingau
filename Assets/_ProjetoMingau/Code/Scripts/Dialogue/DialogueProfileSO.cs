using UnityEngine;

[CreateAssetMenu(fileName = "DialogueProfile", menuName = "Dialogue/DialogueProfile")]
public class DialogueProfileSO: ScriptableObject
{
    public string Name;
    public Sprite DefaultEmotion;
}
