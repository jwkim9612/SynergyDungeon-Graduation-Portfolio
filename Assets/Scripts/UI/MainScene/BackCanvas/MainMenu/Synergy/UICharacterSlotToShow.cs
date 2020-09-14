using UnityEngine;
using UnityEngine.UI;

public class UICharacterSlotToShow : MonoBehaviour
{
    [SerializeField] private Text characterName = null;
    [SerializeField] private Image image = null;
    [SerializeField] private Image costBorder = null;

    public CharacterData characterData { get; set; } = null;

    public void SetCharacterData(CharacterData newCharacterData)
    {
        characterData = newCharacterData;

        SetName(characterData.Name);
        SetImage(characterData.Image);
        SetCostBorder(characterData.Tier);
    }

    public void SetName(string name)
    {
        if (name != null)
        {
            characterName.text = name;
        }
        else
        {
            Debug.Log("No Character Name");
        }
    }

    public void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("No Character Image");
        }
    }

    public void SetCostBorder(Tier tier)
    {
        switch (tier)
        {
            case Tier.One:
                costBorder.sprite = CardService.TIER_1_FRAME_IMAGE;
                break;
            case Tier.Two:
                costBorder.sprite = CardService.TIER_2_FRAME_IMAGE;
                break;
            case Tier.Three:
                costBorder.sprite = CardService.TIER_3_FRAME_IMAGE;
                break;
            case Tier.Four:
                costBorder.sprite = CardService.TIER_4_FRAME_IMAGE;
                break;
            default:
                Debug.Log("Error SetCostBorder");
                break;
        }
    }
}
