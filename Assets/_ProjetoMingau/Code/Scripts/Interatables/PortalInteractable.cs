using System.Threading.Tasks;
using UnityEngine;

public class PortalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private LevelData _targetLevel;

    private bool _hasBeenInteracted = false;
    public bool HasBeenInteracted
    {
        get => _hasBeenInteracted;
        set {  _hasBeenInteracted = value; }
    }

    public async void Interact()
    {
        if (HasBeenInteracted) return;
        await TriggerLevelLoad();
    }

    private async Task TriggerLevelLoad()
    {
        if (LevelManager.I.IsLevelLoading) return;

        if (LevelManager.I.CurrentLoadedLevel.IsGameStage)
        {
            LevelProgressManager.I.EndLevel();
            LevelProgressManager.I.SetNextLevel(_targetLevel);
        }
        else
        {
            await LevelManager.I.LoadLevel(_targetLevel);
        }

        HasBeenInteracted = true;
    }
}
