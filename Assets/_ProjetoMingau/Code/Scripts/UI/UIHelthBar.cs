using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIHelthBar : UIBase
{
    [SerializeField] private CanvasGroup _healthCanvas;

    [SerializeField] private Image[] _healthImages;

    [SerializeField] private Sprite _healthFullSprite;
    [SerializeField] private Sprite _healthEmptySprite;

    public override void Initialize()
    {
        GameContext.I.PlayerAttributes.OnHealthChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        GameContext.I.PlayerAttributes.OnHealthChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int hp)
    {
        foreach(Image healthImage in _healthImages.Reverse())
        {
            if(hp > 0)
            {
                healthImage.sprite = _healthFullSprite;
                hp--;
            }
            else
            {
                healthImage.sprite = _healthEmptySprite;
            }
        }
    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.33f;

        _healthCanvas.alpha = startValue;
        if (!_healthCanvas.gameObject.activeSelf) Toggle(true);

        _healthCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _healthCanvas.alpha = endValue;
            });
    }

    public override void Toggle(bool toggle)
    {
        _healthCanvas.gameObject.SetActive(toggle);
    }
}
