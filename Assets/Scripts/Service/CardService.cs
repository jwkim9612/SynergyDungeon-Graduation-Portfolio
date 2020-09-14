using UnityEngine;

public class CardService
{
    public const int NUM_OF_CARDS = 29;
    public const int MAX_NUM_OF_CARDS_PER_CHARACTER = 9;
    public const int RELOAD_PRICE = 2;
    public const int ADDEXP_PRICE = 1;

    public const string DEFAULT_IMAGE_NAME = "Empty";

    public const string TIER_1_FRAME_IMAGE_PATH = "Images/Character/Frame/Tier_1_Frame";
    public const string TIER_2_FRAME_IMAGE_PATH = "Images/Character/Frame/Tier_2_Frame";
    public const string TIER_3_FRAME_IMAGE_PATH = "Images/Character/Frame/Tier_3_Frame";
    public const string TIER_4_FRAME_IMAGE_PATH = "Images/Character/Frame/Tier_4_Frame";

    public readonly static Sprite TIER_1_FRAME_IMAGE = Resources.Load<Sprite>(TIER_1_FRAME_IMAGE_PATH);
    public readonly static Sprite TIER_2_FRAME_IMAGE = Resources.Load<Sprite>(TIER_2_FRAME_IMAGE_PATH);
    public readonly static Sprite TIER_3_FRAME_IMAGE = Resources.Load<Sprite>(TIER_3_FRAME_IMAGE_PATH);
    public readonly static Sprite TIER_4_FRAME_IMAGE = Resources.Load<Sprite>(TIER_4_FRAME_IMAGE_PATH);

    public static Sprite GetFrameImageByTier(Tier tier)
    {
        Sprite image = null;
        switch (tier)
        {
            case Tier.One:
                image = TIER_1_FRAME_IMAGE;
                break;
            case Tier.Two:
                image = TIER_2_FRAME_IMAGE;
                break;
            case Tier.Three:
                image = TIER_3_FRAME_IMAGE;
                break;
            case Tier.Four:
                image = TIER_4_FRAME_IMAGE;
                break;
            default:
                Debug.Log("Error GetImageByTier");
                break;
        }

        return image;
    }

    public static int GetPriceByTier(Tier tier)
    {
        int price = 0;
        switch (tier)
        {
            case Tier.One:
                price = 1;
                break;
            case Tier.Two:
                price = 2;
                break;
            case Tier.Three:
                price = 3;
                break;
            case Tier.Four:
                price = 4;
                break;
            default:
                Debug.Log("Error GetPriceByTier");
                break;
        }
        return price;
    }
}