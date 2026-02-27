using DG.Tweening;
using UnityEngine;

public class UIInteractionPrompt : UIBase
{
    [SerializeField] private CanvasGroup _interactionCanvas;

    private void Start()
    {
        Toggle(false);
    }

    public override void Initialize()
    {
        GameContext.I.PlayerInteract.OnInteractionAvailableChanged += Transition;
    }
    private void OnDisable()
    {
        GameContext.I.PlayerInteract.OnInteractionAvailableChanged -= Transition;
    }

    public override void Transition(bool toggle)
    {
        float endValue = toggle ? 1f : 0f;
        float startValue = toggle ? 0f : 1f;
        float transitionTime = 0.2f;

        _interactionCanvas.alpha = startValue;
        if (!_interactionCanvas.gameObject.activeSelf) Toggle(true);

        _interactionCanvas.DOFade(endValue, transitionTime)
            .OnComplete(() =>
            {
                Toggle(toggle);
                _interactionCanvas.alpha = endValue;
            });
    }

    public override void Toggle(bool toggle)
    {
        _interactionCanvas.gameObject.SetActive(toggle);
    }
}
