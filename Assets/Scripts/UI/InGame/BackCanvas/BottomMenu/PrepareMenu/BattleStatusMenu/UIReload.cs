using UnityEngine;
using UnityEngine.UI;

public class UIReload : MonoBehaviour
{
    [SerializeField] private Button reloadButton = null;

    public void Initialize()
    {
        UpdateReloadable();

        SetReloadButton();

        InGameManager.instance.playerState.OnCoinChanged += UpdateReloadable;
    }

    private void SetReloadButton()
    {
        reloadButton.onClick.AddListener(() => {
            InGameManager.instance.playerState.UseCoin(CardService.RELOAD_PRICE);

            var characterPurchaseSpace = InGameManager.instance.backCanvas.uiBottomMenu.uiPrepareMenu.uiCharacterPurchaseSpace;
            characterPurchaseSpace.Shuffle();
        });
    }

    public void UpdateReloadable()
    {
        if (IsReloadable())
        {
            reloadButton.interactable = true;
        }
        else
        {
            reloadButton.interactable = false;
        }
    }

    public bool IsReloadable()
    {
        var gameState = InGameManager.instance.gameState;

        if (gameState.IsInBattle())
            return false;

        int currentPlayerCoin = InGameManager.instance.playerState.coin;
        int reloadPrice = CardService.RELOAD_PRICE;

        return currentPlayerCoin >= reloadPrice;
    }
}
