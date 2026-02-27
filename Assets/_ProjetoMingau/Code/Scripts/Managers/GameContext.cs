using Unity.Cinemachine;
using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    public CinemachineCamera CinemachineRef { get; private set; }
    public void SetCinemachineRef(CinemachineCamera value) { CinemachineRef = value; }
    
    public Camera MainCameraRef { get; private set; }
    public void SetMainCameraRef(Camera value) { MainCameraRef = value; }

    public GameObject PlayerRef { get; private set; }
    public void SetPlayerRef(GameObject value) { PlayerRef = value; }

    public PlayerInteract PlayerInteract { get; private set; }
    public PlayerActionCombo PlayerCombo { get; private set; }
    public PlayerSimpleInventory PlayerInventory { get; private set; }
    public AttributesManager PlayerAttributes { get; private set; }


    public void LoadPlayerRefs()
    {
        PlayerInteract = PlayerRef.GetComponent<PlayerInteract>();
        PlayerCombo = PlayerRef.GetComponent<PlayerActionCombo>();
        PlayerInventory = PlayerRef.GetComponent<PlayerSimpleInventory>();
        PlayerAttributes = PlayerRef.GetComponent<AttributesManager>();
    }

    public void TooggleCinemachineInput(bool toggle)
    {
        CinemachineInputAxisController controller = CinemachineRef.GetComponent<CinemachineInputAxisController>();

        controller.enabled = toggle;
    }

    public void ResetCinemachineLookRotation()
    {
        CinemachineOrbitalFollow orbitalFollow = CinemachineRef.GetComponent<CinemachineOrbitalFollow>();

        orbitalFollow.HorizontalAxis.Value = 0;
        orbitalFollow.VerticalAxis.Value = 17.5f;
    }
}
