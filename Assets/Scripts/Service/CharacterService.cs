using UnityEngine;

public class CharacterService
{
    public const int NUM_OF_DEFAULT_STAR = 1;
    public const int NUMBER_REQUIRED_FOR_COMBINATION = 3;
    public const int ID_OF_LAST_CHARACTER = 29;
    public const int ID_OF_FIRST_CHARACTER = 1;

    public const float SIZE_IN_PREPARE_AREA = 1.0f;
    public const float SIZE_IN_BATTLE_AREA = 1.2f;

    public const int DEFAULT_CHARACTER_STAR = 1;

    public static int GetSalePrice(CharacterInfo characterInfo)
    {
        int price = -1;

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if(characterDataSheet == null)
        {
            Debug.LogError("Error characterDataSheet is null");
            return -1;
        }

        Tier tier;

        switch (characterInfo.star)
        {
            case 1:
                if(characterDataSheet.TryGetCharacterTier(characterInfo.id, out tier))
                {
                    price = (int)tier;
                }
                break;
            case 2:
                if (characterDataSheet.TryGetCharacterTier(characterInfo.id, out tier))
                {
                    price = (int)tier + 2;
                }
                break;
            case 3:
                if (characterDataSheet.TryGetCharacterTier(characterInfo.id, out tier))
                {
                    price = (int)tier + 4;
                }
                break;
            default:
                Debug.Log("Error GetSalePrice");
                break;
        }

        if(price <= 0)
        {
            Debug.LogError("Error price is less than 0");
        }

        return price;
    }
}