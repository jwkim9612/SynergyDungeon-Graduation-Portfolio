using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameTopMenu : MonoBehaviour
{
    [SerializeField] private GameObject prepareMenu = null;
    [SerializeField] private GameObject battleMenu = null;
    [SerializeField] private UIWave uiWave = null;
    [SerializeField] private UIStart uiStart = null;

    private void Start()
    {
        InGameManager.instance.gameState.OnBattle += ShowBattleMenu;
        InGameManager.instance.gameState.OnPrepare += ShowPrepareMenu;

        uiWave.Initialize();
        uiStart.Initialize();
    }

    private void ShowBattleMenu()
    {
        battleMenu.SetActive(true);
        prepareMenu.SetActive(false);
    }

    private void ShowPrepareMenu()
    {
        battleMenu.SetActive(false);
        prepareMenu.SetActive(true);
    }
}
