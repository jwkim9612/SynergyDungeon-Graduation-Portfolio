using UnityEngine;
using UnityEngine.UI;

public class UIAddExp : MonoBehaviour
{
    [SerializeField] private Button addExpButton = null;

    public void Initialize()
    {
        UpdateAddExpable();

        SetAddExpButton();

        InGameManager.instance.playerState.OnCoinChanged += UpdateAddExpable;
        InGameManager.instance.playerState.OnLevelUp += UpdateAddExpable;
    }

    public void SetAddExpButton()
    {
        addExpButton.onClick.AddListener(() => {
            InGameManager.instance.playerState.IncreaseExpByAddExp();
        });
    }

    public void UpdateAddExpable()
    {
        if (IsAddExpable())
        {
            addExpButton.interactable = true;
        }
        else
        {
            addExpButton.interactable = false;
        }
    }

    public bool IsAddExpable()
    {
        var gameState = InGameManager.instance.gameState;
        var playerState = InGameManager.instance.playerState;

        if (gameState.IsInBattle() || playerState.IsMaxLevel())
            return false;


        int currentPlayerCoin = InGameManager.instance.playerState.coin;
        int addExpPrice = CardService.ADDEXP_PRICE;

        return currentPlayerCoin >= addExpPrice;
    }
}
