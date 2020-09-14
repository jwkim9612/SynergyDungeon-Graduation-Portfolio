using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public void Initialize()
    {
        if(IsFirstTime())
        {
            PlayerPrefs.SetInt("BGMSound", 100);
            PlayerPrefs.SetInt("SFXSound", 100);
        }

        // Sound On 함수
    }

    public void UpdateBgmSound()
    {
        // bgm 소리 크기 변경
    }

    public void UpdateSfxSound()
    {
        // effect 소리 크기 변경
    }

    public bool IsFirstTime()
    {
        if(!PlayerPrefs.HasKey("BGMSound") || !PlayerPrefs.HasKey("SFXSound"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
