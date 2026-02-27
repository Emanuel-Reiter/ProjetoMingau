using DG.Tweening;
using UnityEngine;

public class UISettingsScreen : UIBase
{
    [SerializeField] private CanvasGroup _settingsCanvas;

    private void Start()
    {
        Toggle(false);
    }

    public override void Initialize()
    {

    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.33f;

        _settingsCanvas.alpha = startValue;
        if (!_settingsCanvas.gameObject.activeSelf) _settingsCanvas.gameObject.SetActive(true);

        _settingsCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _settingsCanvas.alpha = endValue;
            });
    }

    public override void Toggle(bool toggle)
    {
        _settingsCanvas.gameObject.SetActive(toggle);
    }
}
