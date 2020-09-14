using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleAreaSlot : UISlot
{
    [SerializeField] private Image seat = null;

    private void Start()
    {
        InGameManager.instance.gameState.OnBattle += HideSeat;
        InGameManager.instance.gameState.OnPrepare += ShowSeat;
    }

    private void ShowSeat()
    {
        seat.enabled = true;
    }

    private void HideSeat()
    {
        seat.enabled = false;
    }
}
