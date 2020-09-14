using UnityEngine;
using UnityEngine.UI;

public class UIRuneForRunePage : UIRune
{
    [SerializeField] protected Button showRuneInfoButton;

    public void Initialize()
    {
        SetShowRuneInfoButton();
    }

    private void SetShowRuneInfoButton()
    {
        showRuneInfoButton.onClick.AddListener(() =>
        {
            var uiRuneInfo = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneInfo;
            uiRuneInfo.SetUIRuneInfo(rune.runeData, isEquippedRune, this);
            UIManager.Instance.ShowNew(uiRuneInfo);
        });
    }

    protected void SetShowRuneInfoButtonInteractable(bool isInteractable)
    {
        if (isInteractable)
        {
            showRuneInfoButton.interactable = true;
        }
        else
        {
            showRuneInfoButton.interactable = false;
        }
    }
}
