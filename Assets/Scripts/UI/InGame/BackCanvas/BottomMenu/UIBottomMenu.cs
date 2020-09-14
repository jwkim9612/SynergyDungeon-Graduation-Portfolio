using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBottomMenu : MonoBehaviour
{
    public UIBattleMenu uiBattleMenu = null;
    public UIPrepareMenu uiPrepareMenu = null;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += ShowBattleMenu;
        InGameManager.instance.gameState.OnPrepare += ShowPrepareMenu;

        uiBattleMenu.Initialize();
        uiPrepareMenu.Initialize();
    }

    private void ShowBattleMenu()
    {
        uiBattleMenu.OnShow();
        uiPrepareMenu.OnHide();
    }

    private void ShowPrepareMenu()
    {
        uiBattleMenu.OnHide();
        uiPrepareMenu.OnShow();
    }
}
