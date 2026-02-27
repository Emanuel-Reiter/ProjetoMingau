using UnityEngine;
using UnityEngine.UI;

public class UITitleScreen : UIBase
{
    [SerializeField] private CanvasGroup _titleScreenCanvas;
    
    private CanvasScaler _scaler;
    private Button _startButton;
    
    private void Start()
    {
        _scaler = GetComponent<CanvasScaler>();
        Toggle(true);
    }

    public override void Initialize()
    {
        LevelManager.I.OnLevelLoadPercentChanged += DisableTitleScreen;
    }

    private void OnDisable()
    {
        LevelManager.I.OnLevelLoadPercentChanged -= DisableTitleScreen;
    }

    public void StartGameEvent(Button target)
    {
        UIManager.ToggleCursor(false);

        _startButton = target;
        _startButton.interactable = false;

        Invoke("ActivateStartButton", 2f);

        _ = LevelManager.I.InitalizeGame();
    }

    private void ActivateStartButton()
    {
        if (_startButton == null) return;
        _startButton.interactable = true;
    }

    public override void Transition(bool toggle)
    {
        Toggle(toggle);
    }

    public override void Toggle(bool toggle)
    {
        if(_scaler != null)
        {
            if (toggle) _scaler.matchWidthOrHeight = 1f;
            else _scaler.matchWidthOrHeight = 0f;
        }

        _titleScreenCanvas.gameObject.SetActive(toggle);
    }


    private void DisableTitleScreen(float loadPercent)
    {
        if (loadPercent < 1f) return;

        Transition(false);
    }

    public void ExitGameEvent(Button target)
    {
        UIManager.ToggleCursor(false);
        target.interactable = false;
        GlobalTimer.I.StartTimer(1.0f, () => { Application.Quit(); });
    }
}
