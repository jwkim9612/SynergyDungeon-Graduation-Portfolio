using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterPurchaseSpace : MonoBehaviour
{
    [SerializeField] private List<UICharacterCard> cards = null;

    public void Initialize()
    {
        InitializeCardsList();

        Shuffle();
        InGameManager.instance.gameState.OnPrepare += Shuffle;
    }

    private void InitializeCardsList()
    {
        cards = GetComponentsInChildren<UICharacterCard>().ToList();
        
        foreach (var card in cards)
        {
            card.Initialize();
        }
    }

    // 카드 섞기
    public void Shuffle()
    {
        var probabilitySystem = InGameManager.instance.probabilitySystem;
        var characterStockSystem = InGameManager.instance.characterStockSystem;

        foreach (var card in cards)
        {
            if (!(card.isBoughtCard))
            {
                int cardId = card.characterData.Id;
                characterStockSystem.AddCharacterId(cardId);
            }

            Tier randomTier = probabilitySystem.GetRandomTier();
            int randomId = characterStockSystem.GetRandomId(randomTier);

            var characterDataSheet = DataBase.Instance.characterDataSheet;
            if (characterDataSheet.TryGetCharacterData(randomId, out var characterData))
            {
                card.SetCard(characterData);
                card.UpdateBuyable();
                card.OnShow();
                card.isBoughtCard = false;
            }
        }
    }

    /// <summary>
    /// 치트
    /// </summary>
    /// <param name="id"></param>
    public void CheatPurchaseCharacter(int id)
    {
        CharacterInfo characterInfo = new CharacterInfo(id);

        if (InGameManager.instance.combinationSystem.IsUpgradable(characterInfo))
        {
            BuyCharacter(id);
        }
        else
        {
            var emptyUICharacter = InGameManager.instance.draggableCentral.uiPrepareArea.GetEmptyUICharacter();
            if (emptyUICharacter == null)
            {
                Debug.Log("uiCharacter is full");
            }
            else
            {
                emptyUICharacter.SetCharacter(characterInfo);
                BuyCharacter(id);
            }
        }
    }

    private void BuyCharacter(int id)
    {
        InGameManager.instance.combinationSystem.AddCharacter(new CharacterInfo(id));

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet == null)
        {
            Debug.LogError("Error characterDataSheet is null");
            return;
        }

        int price = 0;
        if (characterDataSheet.TryGetCharacterTier(id, out var tier))
        {
            price = CardService.GetPriceByTier(tier);
        }

        InGameManager.instance.playerState.UseCoin(price);
    }

    /////치트
}
