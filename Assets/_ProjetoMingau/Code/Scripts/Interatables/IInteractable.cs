using UnityEngine;

public interface IInteractable
{
    bool HasBeenInteracted { get; set; }

    void Interact();
}
