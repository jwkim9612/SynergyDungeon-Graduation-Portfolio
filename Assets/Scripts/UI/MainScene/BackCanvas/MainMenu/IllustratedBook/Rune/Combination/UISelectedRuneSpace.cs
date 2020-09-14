using UnityEngine;

public class UISelectedRuneSpace : MonoBehaviour
{
    public UISelectedRune uiSelectedRune;

    public void SetUISelectRune(RuneData runeData, UIRuneForCombination uiRuneForCombination)
    {
        uiSelectedRune.SetUIRune(runeData);
        uiSelectedRune.isEquippedRune = uiRuneForCombination.isEquippedRune;
        uiSelectedRune.OnShow();
        uiSelectedRune.SetButton(uiRuneForCombination);
    }

    public void Reset()
    {
        uiSelectedRune.Disable();
        uiSelectedRune.OnHide();
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }
}
