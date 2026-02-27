using DG.Tweening;
using TMPro;
using UnityEngine;

public class UICombo : UIBase
{
    [SerializeField] private CanvasGroup _comboCanvas;
    [SerializeField] private TMP_Text _comboText;

    public override void Initialize()
    {
        GameContext.I.PlayerCombo.OnComboChange += UpdateComboText;
        UpdateComboText(0);
    }

    private void OnDisable()
    {
        GameContext.I.PlayerCombo.OnComboChange -= UpdateComboText;
    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.33f;

        _comboCanvas.alpha = startValue;
        if (!_comboCanvas.gameObject.activeSelf) Toggle(true);

        _comboCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _comboCanvas.alpha = endValue;
            });
    }

    public override void Toggle(bool toggle)
    {
        _comboCanvas.gameObject.SetActive(toggle);
    }

    private void UpdateComboText(int comboIndex)
    {
        if (_comboText == null || _comboCanvas == null) return;

        _comboText.text = $"{comboIndex}";

        if (comboIndex == 0)
        {
            Transition(false);
            return;
        }

        Transition(true);

        ResetUITransforms();

        _comboCanvas.transform.DOScale(2.0f, 0.1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo);

        float rotation = comboIndex % 2 == 0 ? 22.5f : -22.5f;
        _comboCanvas.transform.DORotate(new Vector3(0.0f, 0.0f, rotation), 0.1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => ResetUITransforms());
    }

    private void ResetUITransforms()
    {
        _comboCanvas.transform.localScale = Vector3.one;
        _comboCanvas.transform.rotation = Quaternion.identity;
    }
}
