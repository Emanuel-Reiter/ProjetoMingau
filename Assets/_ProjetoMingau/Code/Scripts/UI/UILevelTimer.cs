using System;
using TMPro;
using UnityEngine;

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

        Toggle(true);

        TimeSpan t = TimeSpan.FromSeconds(time);
        string formattedTime = $"{t.Minutes}m {t.Seconds:D2}s";
        _timerText.text = formattedTime;
    }

    public override void Toggle(bool toggle)
    {
        _timerCanvas.gameObject.SetActive(toggle);
    }
}
