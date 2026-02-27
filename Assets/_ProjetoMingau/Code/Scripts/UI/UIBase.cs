using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Transition(bool toggle);
    public abstract void Toggle(bool toggle);
}
