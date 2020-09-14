using UnityEngine;
using UnityEngine.UI;

public class UISelectedRune : UIRune
{
    [SerializeField] private Button button = null;

    public override void SetUIRune(RuneData runeData)
    {
        base.SetUIRune(runeData);

        button.interactable = true;
    }

    public void SetButton(UIRuneForCombination uiRuneForCombination)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            UISelectedRuneSpace uiSelectedRuneSpace = GetComponentInParent<UISelectedRuneSpace>();
            uiRuneForCombination.Release(uiSelectedRuneSpace);
        });
    }

    public void Disable()
    {
        rune = null;
        button.interactable = false;
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
