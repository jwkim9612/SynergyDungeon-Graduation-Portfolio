using UnityEngine;
using UnityEngine.UI;

public class UICharacterSlot : MonoBehaviour
{
    [SerializeField] private UICharacterInfo characterInfo = null;
    [SerializeField] private Text characterName = null;
    [SerializeField] private Image characterImage = null;
    [SerializeField] private Image tribeImage = null;
    [SerializeField] private Text tribeText = null;
    [SerializeField] private Image originImage = null;
    [SerializeField] private Text originText = null;
    [SerializeField] private Image tierFrameImage = null;
    
    public CharacterData characterData { get; set; }

    public void SetCharacterData(CharacterData newCharacterData)
    {
        characterData = newCharacterData;

        SetName(characterData.Name);
        SetCharacterImage(characterData.Image);
        SetTribeImage(characterData.TribeData.Image);
        SetTribeText(characterData.Tribe);
        SetOriginImage(characterData.OriginData.Image);
        SetOriginText(characterData.Origin);
        SetTierFrameImage(CardService.GetFrameImageByTier(characterData.Tier));
    }

    public void SetName(string name)
    {
        if(name != "")
        {
            characterName.text = name;
        }
        else
        {
            Debug.Log("No Character Name");
        }
    }

    public void SetCharacterImage(Sprite sprite)
    {
        if (sprite != null)
        {
            characterImage.sprite = sprite;
        }
        else
        {
            Debug.Log("No Character Image");
        }
    }

    public void SetTribeImage(Sprite sprite)
    {
        if (sprite != null)
        {
            tribeImage.sprite = sprite;
        }
        else
        {
            Debug.Log($"{characterData.Name} No tribe Image");
        }
    }

    public void SetTribeText(Tribe tribe)
    {
        var tribeStr = SynergyService.GetNameByTribe(tribe);
        tribeText.text = tribeStr;
    }

    public void SetOriginImage(Sprite sprite)
    {
        if (sprite != null)
        {
            originImage.sprite = sprite;
        }
        else
        {
            Debug.Log("No origin Image");
        }
    }

    public void SetOriginText(Origin origin)
    {
        var originStr = SynergyService.GetNameByOrigin(origin);
        originText.text = originStr;
    }

    public void SetTierFrameImage(Sprite image)
    {
        tierFrameImage.sprite = image;
    }

    public void OnClicked()
    {
        characterInfo.SetCharacterData(characterData);
        UIManager.Instance.ShowNew(characterInfo);
    }
}
