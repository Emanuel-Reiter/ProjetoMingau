using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreen : UIBase
{
    [SerializeField] private CanvasGroup _loadingScreenCanvas;
    [SerializeField] private Slider _loadingSlider;

    [SerializeField] protected TMP_Text _curiositiesText;

    [TextArea] [SerializeField] string[] _curiosities;

    private float _loadingProgress;
    private float _loadingSmoothTime = 1f;

    private void Start()
    {
        Toggle(false);
    }

    public override void Initialize()
    {
        LevelManager.I.OnLevelLoadingChanged += Transition;
        LevelManager.I.OnLevelLoadPercentChanged += UpdateLoadingSlider;
    }

    private void OnDisable()
    {
        LevelManager.I.OnLevelLoadingChanged -= Transition;
        LevelManager.I.OnLevelLoadPercentChanged -= UpdateLoadingSlider;
    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.33f;

        _loadingScreenCanvas.alpha = startValue;
        if (!_loadingScreenCanvas.gameObject.activeSelf) _loadingScreenCanvas.gameObject.SetActive(true);

        _loadingScreenCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _loadingScreenCanvas.alpha = endValue;
            });

        _loadingSlider.value = 0;

        if (_curiosities.Length <= 0) return;
        if (!toggle) return;
        _curiositiesText.text = _curiosities[Random.Range(0, _curiosities.Length -1)];
    }

    public override void Toggle(bool toggle)
    {
        _loadingScreenCanvas.gameObject.SetActive(toggle);
    }

    private void UpdateLoadingSlider(float loadPercent)
    {
        _loadingProgress = loadPercent;
    }

    public void Update()
    {
        bool isLoading = LevelManager.I.IsLevelLoading;
        if (!isLoading) return;

        _loadingSlider.value = Mathf.MoveTowards(_loadingSlider.value, _loadingProgress, _loadingSmoothTime * Time.deltaTime);
    }
}
