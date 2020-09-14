using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrepareMenu : MonoBehaviour
{
    public UIBattleSynergyList uiBattleSynergyList;
    public UIBattleStatusMenu uiBattleStatusMenu;
    public UICharacterPurchaseSpace uiCharacterPurchaseSpace;

    public void Initialize()
    {
        uiBattleSynergyList.Initialize();
        uiBattleStatusMenu.Initialize();
        uiCharacterPurchaseSpace.Initialize();
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
