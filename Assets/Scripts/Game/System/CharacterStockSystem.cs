using System.Collections.Generic;
using UnityEngine;

// 인게임에서 정해진 캐릭터의 수를 관리해주는 창고 시스템
public class CharacterStockSystem
{
    public Dictionary<Tier, CharacterStock> Stocks { get; set; }

    private CharacterDataSheet characterDataSheet;

    public CharacterStockSystem()
    {
        Stocks = new Dictionary<Tier, CharacterStock>();
    }

    public void Initialize()
    {
        characterDataSheet = DataBase.Instance.characterDataSheet;
        if (characterDataSheet == null)
        {
            Debug.LogError("Error characterDataSheet is null");
            return;
        }

        InitializeStocks();
    }

    private void InitializeStocks()
    {
        CharacterStock oneTierStock = new CharacterStock();
        CharacterStock twoTierStock = new CharacterStock();
        CharacterStock threeTierStock = new CharacterStock();
        CharacterStock fourTierStock = new CharacterStock();

        if (characterDataSheet.TryGetCharacterDatas(out var characterDatas))
        {
            foreach (var characterData in characterDatas)
            {
                for (int i = 0; i < CardService.MAX_NUM_OF_CARDS_PER_CHARACTER; ++i)
                {
                    switch (characterData.Value.Tier)
                    {
                        case Tier.One:
                            oneTierStock.characterIds.Add(characterData.Key);
                            break;
                        case Tier.Two:
                            twoTierStock.characterIds.Add(characterData.Key);
                            break;
                        case Tier.Three:
                            threeTierStock.characterIds.Add(characterData.Key);
                            break;
                        case Tier.Four:
                            fourTierStock.characterIds.Add(characterData.Key);
                            break;
                        default:
                            Debug.Log("Error InitializeStock");
                            break;
                    }
                }
            }
        }

        Stocks.Add(Tier.One, oneTierStock);
        Stocks.Add(Tier.Two, twoTierStock);
        Stocks.Add(Tier.Three, threeTierStock);
        Stocks.Add(Tier.Four, fourTierStock);
    }

    public void ClearAllStock()
    {
        foreach (var stock in Stocks)
        {
            stock.Value.characterIds.Clear();
        }
    }

    // 창고에서 랜덤으로 아이디를 하나 반환.
    // 반환 하면서 창고에서 그 아이디를 하나를 삭제한다.
    public int GetRandomId(Tier tier)
    {
        CharacterStock stock = Stocks[tier];

        int randomIndex = Random.Range(0, stock.characterIds.Count);
        int randomCharacterId = stock.characterIds[randomIndex];

        RemoveStockId(randomCharacterId);

        return randomCharacterId;
    }

    // 창고에서 해당 캐릭터 아이디 하나를 삭제
    public void RemoveStockId(int characterId)
    {
        if (characterDataSheet.TryGetCharacterTier(characterId, out var tier))
        {
            CharacterStock stock = Stocks[tier];
            stock.characterIds.Remove(characterId);
        }
    }

    // 창고에 캐릭터 별(성)에 따른 캐릭터 아이디 삭제
    public void RemoveStockId(CharacterInfo characterInfo)
    {
        if (characterDataSheet.TryGetCharacterTier(characterInfo.id, out var tier))
        {
            CharacterStock stock = Stocks[tier];

            int numOfAdditions = GetNumOfCharactersPerStar(characterInfo.star);

            for (int i = 0; i < numOfAdditions; ++i)
            {
                stock.characterIds.Remove(characterInfo.id);
            }
        }
    }

    // 창고에 캐릭터 아이디 하나를 추가
    public void AddCharacterId(int characterId)
    {
        if (characterDataSheet.TryGetCharacterTier(characterId, out var tier))
        {
            CharacterStock stock = Stocks[tier];
            stock.characterIds.Add(characterId);
        }
    }

    // 창고에 캐릭터 별(성)에 따른 캐릭터 아이디 추가
    public void AddCharacterId(CharacterInfo characterInfo)
    {
        if (characterDataSheet.TryGetCharacterTier(characterInfo.id, out var tier))
        {
            CharacterStock stock = Stocks[tier];

            int numOfAdditions = GetNumOfCharactersPerStar(characterInfo.star);

            for (int i = 0; i < numOfAdditions; ++i)
            {
                stock.characterIds.Add(characterInfo.id);
            }
        }
    }

    // 별 갯수에 따른 캐릭터 갯수를 반환
    public int GetNumOfCharactersPerStar(int star)
    {
        int numOfCharacters = 0;

        switch (star)
        {
            case 1:
                numOfCharacters = 1;
                break;
            case 2:
                numOfCharacters = 3;
                break;
            case 3:
                numOfCharacters = 9;
                break;
            default:
                Debug.Log("Error AddStockId");
                break;
        }

        return numOfCharacters;
    }
}
