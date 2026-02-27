using UnityEngine;

public class LevelProgressManager : Singleton<LevelProgressManager>
{
    private bool _levelComplete = false;
    public bool LevelComplete
    {
        get => _levelComplete;
        set
        {
            if (_levelComplete == value) return;

            _levelComplete = value;

            if(LevelComplete) OnLevelComplete?.Invoke(LevelStars, LevelCompletionTime, LevelCollectables, LevelScore);
        }
    }

    public delegate void OnLevelCompleteDelegate(int stars, float time, int collectables, int score);
    public event OnLevelCompleteDelegate OnLevelComplete;

    public float LevelCompletionTime { get; private set; } = 0f;
    private float _levelStartTime;
    private float _levelFinishTime;

    public int LevelScore { get; private set; } = 0;

    public int LevelCollectables { get; private set; } = 0;

    private int _levelStars = 1;
    public int LevelStars
    {
        get => _levelStars;
        set
        {
            if(_levelStars == value) return;
            _levelStars = Mathf.Clamp(value, 1, 3);
        }
    }

    private float _levelTimer = 0f;
    public float LevelTimer
    {
        get => _levelTimer;
        set
        {
            _levelTimer = value;
            OnLevelTimerChange?.Invoke(Mathf.RoundToInt(LevelTimer));
        }
    }

    private bool _useTimer = false;

    public delegate void OnLevelTimerChangeDelegate(int levelTimer);
    public event OnLevelTimerChangeDelegate OnLevelTimerChange;

    public LevelData NextLevel { get; private set; }
    public void SetNextLevel(LevelData level) { NextLevel = level; }

    public void EndLevel()
    {
        _levelFinishTime = Time.time;
        LevelCompletionTime = _levelFinishTime - _levelStartTime;

        LevelCollectables = GameContext.I.PlayerInventory.Collectables;

        int oneStarTime = LevelManager.I.CurrentLoadedLevel.OneStarTimeInSeconds;
        int twoStarTime = LevelManager.I.CurrentLoadedLevel.TwoStarTimeInSeconds;
        int threeStarTime = LevelManager.I.CurrentLoadedLevel.ThreeStarTimeInSeconds;
        int completeTime = Mathf.RoundToInt(LevelCompletionTime);

        LevelScore = Mathf.Clamp(oneStarTime - completeTime, 0, 9999999);
        LevelScore = (LevelScore * 10) + (LevelCollectables * 5);

        if (completeTime <= threeStarTime) LevelStars = 3;
        else if (completeTime <= twoStarTime && completeTime > threeStarTime) LevelStars = 2;
        else LevelStars = 1;

        LevelComplete = true;
        _useTimer = false;
    }

    private void Update()
    {
        if (!LevelManager.I.HasGameStarted || !_useTimer)
        {
            LevelTimer = 0f;
            return;
        }

        if (LevelManager.I.CurrentLoadedLevel.IsGameStage)
        {
            LevelTimer += Time.deltaTime;
        }
    }

    public void ResetProgress()
    {
        LevelComplete = false;

        _levelStartTime = Time.time;
        _levelFinishTime = 0f;
        LevelScore = 0;
        LevelCollectables = 0;
        LevelStars = 1;
        LevelTimer = 0f;
        _useTimer = true;

        GameContext.I.PlayerInventory.Collectables = 0;
        GameContext.I.PlayerCombo.ComboIndex = 0;
    }
}
