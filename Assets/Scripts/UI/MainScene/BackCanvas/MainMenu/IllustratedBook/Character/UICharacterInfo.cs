using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterInfo : UIControl
{ 
    [SerializeField] private Text textName = null;
    [SerializeField] private Image image = null;
    [SerializeField] private Image tribeImage = null;
    [SerializeField] private Text tribeText = null;
    [SerializeField] private Image originImage = null;
    [SerializeField] private Text originText = null;
    [SerializeField] private Text textAttack = null;
    [SerializeField] private Text textMagicalAttack = null;
    [SerializeField] private Text textHealth = null;
    [SerializeField] private Text textDefence = null;
    [SerializeField] private Text textMagicDefence = null;
    [SerializeField] private Text textShield = null;
    [SerializeField] private Text textAccuracy = null;
    [SerializeField] private Text textEvasion = null;
    [SerializeField] private Text textCritical = null;
    [SerializeField] private Text textAttackSpeed = null;
    [SerializeField] private List<Text> textPlusValueList = null;

    private CharacterData characterData;

    public void SetCharacterData(CharacterData newCharacterData)
    {
        characterData = newCharacterData;

        SetName(characterData.Name);
        SetImage(characterData.Image);

        int id = characterData.Id;
        int star = 1;

        var characterAbilityData = DataBase.Instance.characterAbilityDataSheet;
        if (characterAbilityData.TryGetCharacterAbilityData(id, star, out var abilityData))
        {
            SetCharacterAbilityText(abilityData);
        }

        SetPlusValue();
        SetSynergyData();
    }

    public void SetName(string name)
    {
        if (name != null)
        {
            textName.text = name;
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

    public void OnOneStarClick()
    {
        int id = characterData.Id;
        int star = 1;

        var characterAbilityData = DataBase.Instance.characterAbilityDataSheet;
        if (characterAbilityData.TryGetCharacterAbilityData(id, star, out var abilityData))
        {
            SetCharacterAbilityText(abilityData);
        }
    }

    public void OnTwoStarClick()
    {
        int id = characterData.Id;
        int star = 2;

        var characterAbilityData = DataBase.Instance.characterAbilityDataSheet;
        if (characterAbilityData.TryGetCharacterAbilityData(id, star, out var abilityData))
        {
            SetCharacterAbilityText(abilityData);
        }
    }

    public void OnThreeStarClick()
    {
        int id = characterData.Id;
        int star = 3;

        var characterAbilityData = DataBase.Instance.characterAbilityDataSheet;
        if (characterAbilityData.TryGetCharacterAbilityData(id, star, out var abilityData))
        {
            SetCharacterAbilityText(abilityData);
        }
    }

    private void SetCharacterAbilityText(CharacterAbilityData characterAbilityData)
    {
        textAttack.text = characterAbilityData.abilityData.Attack.ToString();
        textMagicalAttack.text = characterAbilityData.abilityData.MagicalAttack.ToString();
        textHealth.text = characterAbilityData.abilityData.Health.ToString();
        textDefence.text = characterAbilityData.abilityData.Defence.ToString();
        textMagicDefence.text = characterAbilityData.abilityData.MagicDefence.ToString();
        textShield.text = characterAbilityData.abilityData.Shield.ToString();
        textAccuracy.text = characterAbilityData.abilityData.Accuracy.ToString();
        textEvasion.text = characterAbilityData.abilityData.Evasion.ToString();
        textCritical.text = characterAbilityData.abilityData.Critical.ToString();
        textAttackSpeed.text = characterAbilityData.abilityData.AttackSpeed.ToString();
    }

    private void SetPlusValue()
    {
        Rune rune = RuneManager.Instance.GetEquippedRuneOfOrigin(characterData.Origin);
        if (rune != null)
        {
            var abilityList = rune.runeData.AbilityData.GetAbilityDataList();

            for (int i = 0; i < abilityList.Count; ++i)
            {
                if (abilityList[i] == 0)
                    textPlusValueList[i].text = "";
                else
                    textPlusValueList[i].text = "+ " + abilityList[i];
            }
        }
        else
        {
            for (int i = 0; i < textPlusValueList.Count; ++i)
            {
                textPlusValueList[i].text = "";
            }
        }
    }

    private void SetSynergyData()
    {
        var tribeDataSheet = DataBase.Instance.tribeDataSheet;
        if(tribeDataSheet == null)
        {
            Debug.Log("tribeDataSheet is null!");
            return;
        }

        var originDataSheet = DataBase.Instance.originDataSheet;
        if (originDataSheet == null)
        {
            Debug.Log("originDataSheet is null!");
            return;
        }

        if(tribeDataSheet.TryGetTribeData(characterData.Tribe, out var tribeData))
        {
            tribeImage.sprite = tribeData.Image;
            tribeText.text = SynergyService.GetNameByTribe(tribeData.Tribe);
        }

        if (originDataSheet.TryGetOriginData(characterData.Origin, out var originData))
        {
            originImage.sprite = originData.Image;
            originText.text = SynergyService.GetNameByOrigin(originData.Origin);
        }
    }
}
