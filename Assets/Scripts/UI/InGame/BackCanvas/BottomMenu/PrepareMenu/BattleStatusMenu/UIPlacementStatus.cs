using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlacementStatus : MonoBehaviour
{
    [SerializeField] private Text placementStatusText = null;

    public void Initialize()
    {
        UpdatePlacementStatus();
        OnShow();

        InGameManager.instance.draggableCentral.uiCharacterArea.OnPlacementChanged += UpdatePlacementStatus;
        InGameManager.instance.playerState.OnLevelUp += UpdatePlacementStatus;
        InGameManager.instance.gameState.OnPrepare += OnShow;
        InGameManager.instance.gameState.OnBattle += OnHide;
    }

    public void UpdatePlacementStatus()
    {
        int numOfCurrentPlacedCharacters = InGameManager.instance.draggableCentral.uiCharacterArea.numOfCurrentPlacedCharacters;
        int numOfCanPlacedInBattleArea = InGameManager.instance.playerState.numOfCanPlacedInBattleArea;

        placementStatusText.text = numOfCurrentPlacedCharacters + "/" + numOfCanPlacedInBattleArea;

        if(numOfCurrentPlacedCharacters == numOfCanPlacedInBattleArea)
        {
            placementStatusText.color = Color.gray - new Color(0, 0, 0, 0.2f);
        }
        else if(numOfCurrentPlacedCharacters < numOfCanPlacedInBattleArea)
        {
            placementStatusText.color = Color.blue - new Color(0, 0, 0, 0.2f);
        }
    }

    public void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
