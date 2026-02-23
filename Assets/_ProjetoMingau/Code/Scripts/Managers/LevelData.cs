using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level scene")]
    [SerializeField] private string _sceneName;
    public string SceneName => _sceneName;

    public bool IsValid => !string.IsNullOrWhiteSpace(_sceneName);

    [Header("Music")]
    [SerializeField] private AudioClip _levelMusic;
    public AudioClip LevelMusic => _levelMusic;

    [Header("Level completion")]
    [SerializeField] private bool _isGameStage = true;
    public bool IsGameStage => _isGameStage;

    [SerializeField] private int _oneStarTimeInSeconds = 120;
    public int OneStarTimeInSeconds => _oneStarTimeInSeconds;

    [SerializeField] private int _twoStarTimeInSeconds = 90;
    public int TwoStarTimeInSeconds => _twoStarTimeInSeconds;

    [SerializeField] private int _threeStarTimeInSeconds = 60;
    public int ThreeStarTimeInSeconds => _threeStarTimeInSeconds;
}
