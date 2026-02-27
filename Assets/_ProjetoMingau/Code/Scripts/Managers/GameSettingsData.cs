using System;

[Serializable]
public class GameSettingsData
{
    // Camera settings
    public float Camera_Speed;

    // Audio settings
    public int Volume_Audio;
    public int Volume_Music;
    public int Volume_Collectables;

    // Controls settings


    // Video settings
    public bool Video_UseFullScreen;
    public bool Video_UseWindowed;
    public int Video_RenderResolution;
    public bool Video_UseVSync;
    public float Video_RenderScale;

    // Graphics settings
    public int GFX_ShadowQuality;
    public int GFX_AOQuality;
    public int GFX_AAQuality;
    public int GFX_TextureQuality;
}