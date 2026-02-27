using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    // Interaction prompt
    private UIInteractionPrompt _interactionPrompt;

    // HUD
    private UICombo _comboHUD;
    private UICollectables _collectablesHUD;
    private UILevelTimer _levelTimerHUD;
    private UIHelthBar _healthBarHUD;

    // Menus
    private UITitleScreen _titleScreenMenu;
    private UILoadingScreen _loadingScreen;
    private UILevelCompleteScreen _levelCompleteScreen;

    private UIDialogueScreen _dialogueScreen;
    public UIDialogueScreen DialogueScreen => _dialogueScreen;

    private UISettingsScreen _settingsScreen;


    [Header("UI visual params")]
    [SerializeField] private float _buttonSelectScale = 1.1f;
    [SerializeField] private float _buttonConfirmScale = 1.2f;
    [SerializeField] private float _buttonTransitionTime = 0.2f;

    [Header("UI SFX")]
    [SerializeField] private AudioSfxDef _confirmAudio;
    [SerializeField] private AudioSfxDef _selectAudio;

    static private bool _onMenu = true;
    static public bool OnMenu
    {
        get => _onMenu;
        set
        {
            if (value == _onMenu) return;
            _onMenu = value;
            OnMenuChanged?.Invoke(OnMenu);
        }
    }

    public delegate void OnMenuChangedDelegate(bool onMenu);
    public static event OnMenuChangedDelegate OnMenuChanged;

    public static void ToggleCursor(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnMenu = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            OnMenu = false;
        }
    }

    public static void TooglePause(bool value)
    {
        if (value) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    #region Initialization
    public void InitializeInteractPrompt()
    {
        if (!LoadInteractionPrompt())
        {
            Debug.LogError("Error loading interaction prompt.");
            return;
        }

        _interactionPrompt.Initialize();
    }

    private bool LoadInteractionPrompt()
    {
        _interactionPrompt = FindFirstObjectByType<UIInteractionPrompt>();
        
        if (CatchNull(_interactionPrompt)) return false;
        else return true;
    }

    public void InitializeHUD()
    {
        if (!LoadHUD())
        {
            Debug.LogError("Error loading HUD.");
            return;
        }

        _comboHUD.Initialize();
        _collectablesHUD.Initialize();
        _levelTimerHUD.Initialize();
        _healthBarHUD.Initialize();
    }

    private bool LoadHUD()
    {
        _comboHUD = FindFirstObjectByType<UICombo>();
        _collectablesHUD = FindFirstObjectByType<UICollectables>();
        _levelTimerHUD = FindFirstObjectByType<UILevelTimer>();
        _healthBarHUD = FindFirstObjectByType<UIHelthBar>();

        if (CatchNull(_comboHUD, _collectablesHUD, _levelTimerHUD, _healthBarHUD)) return false;
        else return true;
    }

    public void InitializeMenus()
    {
        if (!LoadMenus())
        {
            Debug.LogError("Error loading menus.");
            return;
        }

        _titleScreenMenu.Initialize();
        _loadingScreen.Initialize();
        _dialogueScreen.Initialize();
        _levelCompleteScreen.Initialize();
        _settingsScreen.Initialize();
    }

    private bool LoadMenus()
    {
        _titleScreenMenu = FindFirstObjectByType<UITitleScreen>();
        _loadingScreen = FindFirstObjectByType<UILoadingScreen>();
        _dialogueScreen = FindFirstObjectByType<UIDialogueScreen>();
        _levelCompleteScreen = FindFirstObjectByType<UILevelCompleteScreen>();
        _settingsScreen = FindFirstObjectByType<UISettingsScreen>();

        if (CatchNull(_titleScreenMenu, _loadingScreen, _dialogueScreen, _levelCompleteScreen, _settingsScreen)) return false;
        else return true;
    }

    public static bool CatchNull(params Object[] objects)
    {
        foreach (var obj in objects)
        {
            if (obj == null)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Effects
    public void OnConfirmButtonEvent(Button target)
    {
        PlayConfirmSFX();

        target.interactable = false;

        target.transform.localScale = Vector3.one;
        target.transform.DOScale(_buttonConfirmScale, _buttonTransitionTime)
        .SetEase(Ease.InOutSine)
        .SetLoops(2, LoopType.Yoyo)
        .OnComplete(() => target.transform.DOKill());
    }

    public void OnClcikButtonEvent(Button target)
    {
        PlayConfirmSFX();

        target.transform.localScale = Vector3.one;
        target.transform.DOScale(_buttonConfirmScale, _buttonTransitionTime)
        .SetEase(Ease.InOutSine)
        .SetLoops(2, LoopType.Yoyo)
        .OnComplete(() => target.transform.DOKill());
    }

    public void OnMouseEnterEvent(Button target)
    {
        if (target == null) return;
        if (!target.interactable) return;

        target.transform.localScale = Vector3.one;
        Tweener tween = target.transform.DOScale(_buttonSelectScale, _buttonTransitionTime)
            .SetEase(Ease.InOutSine);
        
        PlaySelectAudio();
    }

    public void OnMouseExitEvent(Button target)
    {
        if (target == null) return;
        if (!target.interactable) return;

        target.transform.localScale = Vector3.one * _buttonSelectScale;
        target.transform.DOScale(1.0f, _buttonTransitionTime)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => target.transform.DOKill());
    }
    #endregion

    #region Audio
    private void PlayConfirmSFX()
    {
        if(_confirmAudio == null) return;

        AudioSfxDef audio = Instantiate(_confirmAudio);
        AudioPool.Play(audio);
    }

    private void PlaySelectAudio()
    {
        if (_selectAudio == null) return;

        AudioSfxDef audio = Instantiate(_selectAudio);
        AudioPool.Play(audio);
    }
    #endregion
}