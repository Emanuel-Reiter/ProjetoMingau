using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class UILevelTimer : UIBase
{
    [SerializeField] private CanvasGroup _timerCanvas;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        Toggle(false);
    }

    public override void Initialize()
    {
        LevelProgressManager.I.OnLevelTimerChange += UpdateTimer;
    }

    private void OnDisable()
    {
        LevelProgressManager.I.OnLevelTimerChange -= UpdateTimer;
    }

    private void UpdateTimer(int time)
    {
        if(time == 0)
        {
            Toggle(false);
            return;
        }
        
        if(!_timerCanvas.gameObject.activeSelf) Transition(true);

        TimeSpan t = TimeSpan.FromSeconds(time);
        string formattedTime = $"{t.Minutes}m {t.Seconds:D2}s";
        _timerText.text = formattedTime;
    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.33f;

        _timerCanvas.alpha = startValue;
        if (!_timerCanvas.gameObject.activeSelf) Toggle(true);

        _timerCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _timerCanvas.alpha = endValue;
            });
    }

    public override void Toggle(bool toggle)
    {
        _timerCanvas.gameObject.SetActive(toggle);
    }
}
