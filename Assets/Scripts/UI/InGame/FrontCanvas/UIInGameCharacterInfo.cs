using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameCharacterInfo : MonoBehaviour
{
    [SerializeField] private Image characterImage = null;
    [SerializeField] private Text nameText = null;
    [SerializeField] private Image tribeImage = null;
    [SerializeField] private Image originImage = null;

    [SerializeField] private List<Text> abilityTextList;

    //[SerializeField] private Text textAttack = null;
    //[SerializeField] private Text textMagicalAttack = null;
    //[SerializeField] private Text textHealth = null;
    //[SerializeField] private Text textDefence = null;
    //[SerializeField] private Text textMagicDefence = null;
    //[SerializeField] private Text textShield = null;
    //[SerializeField] private Text textAccuracy = null;
    //[SerializeField] private Text textEvasion = null;
    //[SerializeField] private Text textCritical = null;
    //[SerializeField] private Text textAttackSpeed = null;

    [SerializeField] private HorizontalLayoutGroup stars = null;
    private List<Image> starList = null;

    public void Initialize()
    {
        SetStarList();
    }

    private void SetStarList()
    {
        starList = stars.GetComponentsInChildren<Image>().ToList();
    }

    public void SetInGameCharacterInfo(UICharacter uiCharacter)
    {
        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if(characterDataSheet.TryGetCharacterData(uiCharacter.characterInfo.id, out var characterData))
        {
            // 후에 변경 필요
            if(characterData.HeadImage == null)
            {
                SetCharacterImage(characterData.Image);
            }
            else
            {
                SetCharacterImage(characterData.HeadImage);
            }

            SetNameText(characterData.Name);
            SetTribeImage(characterData.TribeData.Image);
            SetOriginImage(characterData.OriginData.Image);
            SetStarGrade(uiCharacter.characterInfo.star);

            var characterAbilityDataSheet = DataBase.Instance.characterAbilityDataSheet;
            if(characterAbilityDataSheet.TryGetCharacterAbilityData(uiCharacter.characterInfo.id, uiCharacter.characterInfo.star, out var abilityData))
            {
                AbilityData defaultAbilityData = new AbilityData();
                defaultAbilityData.SetAbilityData(abilityData);

                SetCharacterAbilityText(defaultAbilityData, uiCharacter.character.ability);
            }
        }
    }

    private void SetCharacterAbilityText(AbilityData defaultData, AbilityData currentData)
    {
        for(int i = 0; i < abilityTextList.Count; ++i)
        {
            abilityTextList[i].text = defaultData[i].ToString();

            if (defaultData[i] < currentData[i])
                abilityTextList[i].text += $" <color=#008000ff>+ {currentData[i] - defaultData[i]}</color>";
            else if (defaultData[i] > currentData[i])
                abilityTextList[i].text += $" <color=#ff0000ff>- {defaultData[i] - currentData[i]}</color>";
        }

        //textAttack.text = defaultData.Attack.ToString();
        //textMagicalAttack.text = defaultData.MagicalAttack.ToString();
        //textHealth.text = defaultData.Health.ToString();
        //textDefence.text = defaultData.Defence.ToString();
        //textMagicDefence.text = defaultData.MagicDefence.ToString();
        //textShield.text = defaultData.Shield.ToString();
        //textAccuracy.text = defaultData.Accuracy.ToString();
        //textEvasion.text = defaultData.Evasion.ToString();
        //textCritical.text = defaultData.Critical.ToString();
        //textAttackSpeed.text = defaultData.AttackSpeed.ToString();

        //if(defaultData.Attack < currentData.Attack)
        //    textAttack.text += $" + {currentData.Attack - defaultData.Attack}";
        //else if(defaultData.Attack > currentData.Attack)
        //    textAttack.text += $" - {defaultData.Attack - currentData.Attack}";

        //if (defaultData.MagicalAttack < currentData.MagicalAttack)
        //    textMagicalAttack.text += $" + {currentData.MagicalAttack - defaultData.MagicalAttack}";
        //else if (defaultData.MagicalAttack > currentData.MagicalAttack)
        //    textMagicalAttack.text += $" - {defaultData.MagicalAttack - currentData.MagicalAttack}";

        //if (defaultData.Health < currentData.Health)
        //    textHealth.text += $" + {currentData.Health - defaultData.Health}";
        //else if (defaultData.Health > currentData.Health)
        //    textHealth.text += $" - {defaultData.Health - currentData.Health}";

        //if (defaultData.Defence < currentData.Defence)
        //    textDefence.text += $" + {currentData.Defence - defaultData.Defence}";
        //else if (defaultData.Defence > currentData.Defence)
        //    textDefence.text += $" - {defaultData.Defence - currentData.Defence}";

        //if (defaultData.MagicDefence < currentData.MagicDefence)
        //    textMagicDefence.text += $" + {currentData.MagicDefence - defaultData.MagicDefence}";
        //else if (defaultData.MagicDefence > currentData.MagicDefence)
        //    textMagicDefence.text += $" - {defaultData.MagicDefence - currentData.MagicDefence}";

        //if (defaultData.Attack < currentData.Attack)
        //    textShield.text += $" + {currentData.Attack - defaultData.Attack}";
        //else if (defaultData.Attack > currentData.Attack)
        //    textShield.text += $" - {defaultData.Attack - currentData.Attack}";
    }

    private void SetStarGrade(int star)
    {
        for (int i = 0; i < starList.Count; ++i)
        {
            if (i < star)
            {
                starList[i].gameObject.SetActive(true);
            }
            else
            {
                starList[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetCharacterImage(Sprite sprite)
    {
        if(sprite == null)
        {
            Debug.LogWarning("Error SetCharacterImage");
            return;
        }

        characterImage.sprite = sprite;
    }

    private void SetNameText(string name)
    {
        nameText.text = name;
    }

    private void SetTribeImage(Sprite sprite)
    {
        tribeImage.sprite = sprite;
    }

    private void SetOriginImage(Sprite sprite)
    {
        originImage.sprite = sprite;
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
