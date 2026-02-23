using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelCompleteScreen : UIBase
{
    [Header("Panel")]
    [SerializeField] private CanvasGroup _levelCompletePanel;

    [Header("Text")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _collectablesText;
    [SerializeField] private TMP_Text _scoreText;

    [Header("Stars")]
    [SerializeField] private Image _star1;
    [SerializeField] private Image _star2;
    [SerializeField] private Image _star3;

    [Header("Sprites")]
    [SerializeField] private Sprite _starFullSprite;
    [SerializeField] private Sprite _starEmptySprite;

    private void Start()
    {
        Toggle(false);
    }

    public override void Initialize()
    {
        LevelProgressManager.I.OnLevelComplete += LoadLevelCompletionScreen;
    }

    private void OnDisable()
    {
        LevelProgressManager.I.OnLevelComplete -= LoadLevelCompletionScreen;
    }

    private void LoadLevelCompletionScreen(int stars, float time, int collectables, int score)
    {
        // Time formatting
        TimeSpan t = TimeSpan.FromSeconds(time);
        string formattedTime = $"{t.Minutes}m {t.Seconds:D2}s";

        _timeText.text = $"Tempo: {formattedTime}";
        _collectablesText.text = $"Coletáveis: {collectables}";
        _scoreText.text = $"Pontuação: {score}";

        if (stars == 1)
        {
            _star1.sprite = _starFullSprite;
            _star2.sprite = _starEmptySprite;
            _star3.sprite = _starEmptySprite;
        }

        if (stars == 2)
        {
            _star1.sprite = _starFullSprite;
            _star2.sprite = _starFullSprite;
            _star3.sprite = _starEmptySprite;
        }

        if (stars == 3)
        {
            _star1.sprite = _starFullSprite;
            _star2.sprite = _starFullSprite;
            _star3.sprite = _starFullSprite;
        }

        Toggle(true);
        UIManager.ToggleCursor(true);
        //UIManager.TooglePause(true);
    }

    public void FinishLevelEvent()
    {
        if (LevelManager.I.IsLevelLoading) return;

        if(LevelProgressManager.I.NextLevel == null)
        {
            Debug.LogError("No level progress manager nextLevel to load.");
        }
        
        Toggle(false);
        UIManager.ToggleCursor(false);
        UIManager.TooglePause(false);
        _ = LevelManager.I.LoadLevel(LevelProgressManager.I.NextLevel);
    }

    public override void Toggle(bool toggle)
    {
        _levelCompletePanel.gameObject.SetActive(toggle);
    }
}
