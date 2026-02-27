using UnityEngine;

public class NPCPerigoDialogueManager : NPCDialgoueBase, IInteractable
{
    private bool _hasBeenInteracted = false;
    public bool HasBeenInteracted
    {
        get => _hasBeenInteracted;
        set { _hasBeenInteracted = value; }
    }

    [SerializeField] private DialogueProfileSO _perigoProfile;

    [SerializeField] private DialogueSO[] _welcomeDialogue;

    public void Interact()
    {
        StartDialogue();
    }

    public override void StartDialogue()
    {
        HasBeenInteracted = true;
        UIManager.I.DialogueScreen.StartDialogue(_perigoProfile, _welcomeDialogue, () => { HasBeenInteracted = false; });
    }
}
