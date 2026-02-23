using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueScreen : UIBase
{
    [SerializeField] private CanvasGroup _dialogueCanvas;

    [Header("Primary speaker")]
    [SerializeField] private Image _speakerImagePrimary;
    [SerializeField] private CanvasGroup _speakerPanelPrimary;
    [SerializeField] private TMP_Text _speakerNamePrimary;
    [SerializeField] private TMP_Text _speakerTextPrimary;
    [SerializeField] private TMP_Text _speakerButtonPrimary;

    [Header("Secondary speaker")]
    [SerializeField] private Image _speakerImageSecondary;
    [SerializeField] private CanvasGroup _speakerPanelSecondary;
    [SerializeField] private TMP_Text _speakerNameSecondary;
    [SerializeField] private TMP_Text _speakerTextSecondary;
    [SerializeField] private TMP_Text _speakerButtonSecondary;

    private DialogueProfileSO _speakerPrimary;
    private DialogueSO[] _dialogueSequence; 
    private int _dialogueIndex = 0;
    private Action _daialogueCallback;

    private void Start()
    {
        Toggle(false);
    }

    public void StartDialogue(DialogueProfileSO speakerPrimary, DialogueSO[] newDialogueSequence, Action callback)
    {
        _dialogueSequence = newDialogueSequence;
        _dialogueIndex = 0;
        _daialogueCallback = callback;
        _speakerPrimary = speakerPrimary;

        UIManager.ToggleCursor(true);

        Toggle(true);
        LoadDialogue();
    }

    private void LoadDialogue()
    {
        bool isPrimarySpeaker = _dialogueSequence[_dialogueIndex].Speaker == _speakerPrimary;
        TogglePrimary(isPrimarySpeaker);
        ToggleSecondary(!isPrimarySpeaker);
    }

    private void TogglePrimary(bool toggle)
    {
        if (toggle)
        {
            // Checks if there is a custom emotion, if not use  the default one
            if (_dialogueSequence[_dialogueIndex].CustomEmotion != null)
            {
                _speakerImagePrimary.sprite = _dialogueSequence[_dialogueIndex].CustomEmotion;
            }
            else
            {
                _speakerImagePrimary.sprite = _dialogueSequence[_dialogueIndex].Speaker.DefaultEmotion;
            }

            _speakerNamePrimary.text = _dialogueSequence[_dialogueIndex].Speaker.Name;
            _speakerTextPrimary.text = _dialogueSequence[_dialogueIndex].DialogueText;

            if (_dialogueIndex == _dialogueSequence.Length - 1)
            {
                _speakerButtonPrimary.text = "Sair";
            }
            else
            {
                _speakerButtonPrimary.text = "Próximo";
            }
        }

        _speakerImagePrimary.gameObject.SetActive(toggle);
        _speakerPanelPrimary.gameObject.SetActive(toggle);
    }

    private void ToggleSecondary(bool toggle)
    {
        if (toggle)
        {
            // Checks if there is a custom emotion, if not use  the default one
            if (_dialogueSequence[_dialogueIndex].CustomEmotion != null)
            {
                _speakerImageSecondary.sprite = _dialogueSequence[_dialogueIndex].CustomEmotion;
            }
            else
            {
                _speakerImageSecondary.sprite = _dialogueSequence[_dialogueIndex].Speaker.DefaultEmotion;
            }

            _speakerNameSecondary.text = _dialogueSequence[_dialogueIndex].Speaker.Name;
            _speakerTextSecondary.text = _dialogueSequence[_dialogueIndex].DialogueText;

            if(_dialogueIndex == _dialogueSequence.Length - 1)
            {
                _speakerButtonSecondary.text = "Sair";
            }
            else
            {
                _speakerButtonSecondary.text = "Próximo";
            }
        }

        _speakerImageSecondary.gameObject.SetActive(toggle);
        _speakerPanelSecondary.gameObject.SetActive(toggle);
    }

    public void NextDialogue()
    {
        _dialogueIndex++;

        if(_dialogueIndex > _dialogueSequence.Length - 1)
        {
            EndDialogue();
            return;
        }

        LoadDialogue();
    }

    private void EndDialogue()
    {
        _daialogueCallback?.Invoke();
        UIManager.ToggleCursor(false);
        Toggle(false);
    }

    public override void Initialize()
    {

    }

    public override void Toggle(bool toggle)
    {
        _dialogueCanvas.gameObject.SetActive(toggle);
    }
}
