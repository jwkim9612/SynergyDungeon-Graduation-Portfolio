using UnityEngine;
using UnityEngine.UI;

public class UIMainOption : UIControl
{
    [SerializeField] private Slider bgmSoundSlider = null;
    [SerializeField] private Slider sfxSoundSlider = null;

    void Start()
    {
        bgmSoundSlider.value = PlayerPrefs.GetInt("BGMSound");
        sfxSoundSlider.value = PlayerPrefs.GetInt("SFXSound");
    }

    public void OnBGMSoundValueChanged()
    {
        PlayerPrefs.SetInt("BGMSound", (int)bgmSoundSlider.value);
        SoundManager.Instance.UpdateBgmSound();
    }

    public void OnEffectSoundValueChanged()
    {
        PlayerPrefs.SetInt("SFXSound", (int)sfxSoundSlider.value);
        SoundManager.Instance.UpdateSfxSound();
    }
}
