using UnityEngine;
using UnityEngine.UI;

public class UITitleScreen : UIBase
{
    [SerializeField] private CanvasGroup _titleScreenCanvas;

    public override void Initialize()
    {
        Toggle(true);
        LevelManager.I.OnLevelLoadPercentChanged += DisableTitleScreen;
    }

    private void OnDisable()
    {
        LevelManager.I.OnLevelLoadPercentChanged -= DisableTitleScreen;
    }

    public void StartGameEvent(Button target)
    {
        UIManager.ToggleCursor(false);
        target.interactable = false;
        _ = LevelManager.I.InitalizeGame();
    }

    public override void Toggle(bool toggle)
    {
        _titleScreenCanvas.gameObject.SetActive(toggle);
    }

    private void DisableTitleScreen(float loadPercent)
    {
        if (loadPercent < 1f) return;

        Toggle(false);
    }

    public void ExitGameEvent(Button target)
    {
        UIManager.ToggleCursor(false);
        target.interactable = false;
        GlobalTimer.I.StartTimer(1.0f, () => { Application.Quit(); });
    }
}
