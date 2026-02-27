using DG.Tweening;
using TMPro;
using UnityEngine;

public class UICollectables : UIBase
{
    [SerializeField] private CanvasGroup _collectablesCanvas;
    [SerializeField] private TMP_Text _collectablesText;

    public override void Initialize()
    {
        GameContext.I.PlayerInventory.OnCollectablesChanged += UpdateCollectablesText;
        UpdateCollectablesText(0);
    }

    private void OnDisable()
    {
        GameContext.I.PlayerInventory.OnCollectablesChanged -= UpdateCollectablesText;
    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.33f;

        _collectablesCanvas.alpha = startValue;
        if (!_collectablesCanvas.gameObject.activeSelf) Toggle(true);

        _collectablesCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _collectablesCanvas.alpha = endValue;
            });
    }

    public override void Toggle(bool toggle)
    {
        _collectablesCanvas.gameObject.SetActive(toggle);
    }

    private void UpdateCollectablesText(int collectables)
    {
        if (_collectablesText == null || _collectablesCanvas == null) return;

        _collectablesText.text = $"x {collectables}";

        if (collectables == 0) return;

        ResetUITransforms();

        _collectablesCanvas.transform.DOScale(2.0f, 0.1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo);

        float rotation = collectables % 2 == 0 ? 22.5f : -22.5f;
        _collectablesCanvas.transform.DORotate(new Vector3(0.0f, 0.0f, rotation), 0.1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => ResetUITransforms());
    }

    private void ResetUITransforms()
    {
        _collectablesCanvas.transform.localScale = Vector3.one;
        _collectablesCanvas.transform.rotation = Quaternion.identity;
    }
}
