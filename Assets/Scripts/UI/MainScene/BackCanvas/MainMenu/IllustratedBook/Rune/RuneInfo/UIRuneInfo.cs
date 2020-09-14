using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRuneInfo : UIControl
{
    [SerializeField] private Text nameText = null;
    [SerializeField] private Text gradeText = null;
    [SerializeField] private Image runeImage = null;
    [SerializeField] private Text descriptionText = null;
    [SerializeField] private Button sellButton = null;
    [SerializeField] private Button equipAndReleaseButton = null;
    [SerializeField] private Text equipAndReleaseText = null;

    private List<UIAttributeInfo> attributeInfoList;

    private UIRune uiRune;
    private bool isEquippedRune;

    public void Initialize()
    {
        SetAttributeInfoList();
        SetSellButton();
    }

    private void SetAttributeInfoList()
    {
        attributeInfoList = GetComponentsInChildren<UIAttributeInfo>().ToList();
    }

    public void SetUIRuneInfo(RuneData runeData, bool isEquippedRune, UIRune uiRune)
    {
        SetName(runeData.Name);
        SetGrade(RuneService.GetNameStrByGrade(runeData.Grade));
        SetImage(runeData.Image);
        SetDescription(runeData.Description);
        SetEquipAndReleaseButtonAndText(isEquippedRune);
        SetAttribute(runeData.AbilityData);

        this.uiRune = uiRune;
        this.isEquippedRune = isEquippedRune;
    }

    private void SetName(string name)
    {
        nameText.text = name;
    }

    private void SetGrade(string grade)
    {
        gradeText.text = grade;
    }

    private void SetImage(Sprite sprite)
    {
        runeImage.sprite = sprite;
    }

    private void SetDescription(string description)
    {
        descriptionText.text = description;
    }
    
    private void SetEquipAndReleaseButtonAndText(bool isEquipped)
    {
        if (isEquipped)
        {
            SetReleaseButton();
            equipAndReleaseText.text = "룬 해제";
        }
        else
        {
            SetEquipButton();
            equipAndReleaseText.text = "룬 장착";
        }
    }

    private void SetEquipButton()
    {
        equipAndReleaseButton.onClick.RemoveAllListeners();
        equipAndReleaseButton.onClick.AddListener(() =>
        {
            EquipRuneAndSubsequentProcessing();

            uiRune.gameObject.SetActive(false);
            //Destroy(uiRune.gameObject);

            UIManager.Instance.HideAndShowPreview();
        });
    }

    private void SetReleaseButton()
    {
        equipAndReleaseButton.onClick.RemoveAllListeners();
        equipAndReleaseButton.onClick.AddListener(() =>
        {
            if (uiRune.rune != null)
            {
                SaveManager.Instance.SetEquippedRuneIdsSaveDataByRelease(uiRune.rune.runeData.SocketPosition);
                SaveManager.Instance.SaveEquippedRuneIds();
                RuneManager.Instance.RemoveEquippedRune(uiRune.rune.runeData.SocketPosition);
                MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRunesOnRunePage.AddUIRune(uiRune.rune.runeData.Id);
                MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination.uiRunesForCombination.RemoveEquipped(uiRune.rune.runeData.Id);
                var uiEquippedRune = uiRune as UIEquipRune;
                uiEquippedRune.Disable();

                UIManager.Instance.HideAndShowPreview();
            }
            else
            {
                Debug.Log("error 장비 해제");
            }
        });
    }

    private void EquipRuneAndSubsequentProcessing()
    {
        var runePage = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage;
        var equipResult = runePage.uiEquippedRunes.EquipRuneAndGetReplaceResult(uiRune as UIRuneOnRunePage);
        if (equipResult.IsReplaced)    // 장착한 곳에 룬이 있었는지 없었는지.
        {
            var equippedRuneData = equipResult.EquippedRune.runeData;
            var equippedRuneId = equippedRuneData.Id;
            var uiRunesForCombination = MainManager.instance.backCanvas.uiMainMenu.uiIllustratedBook.uiRunePage.uiRuneCombination.uiRunesForCombination;

            runePage.uiRunesOnRunePage.AddUIRune(equippedRuneData);
            uiRunesForCombination.RemoveEquipped(equippedRuneId);
        }

        runePage.uiRunesOnRunePage.Sort();
    }

    private void SetAttribute(AbilityData abilityData)
    {
        int attributeInfoIndex = 0;

        var abilityDataList = abilityData.GetAbilityDataList();

        for (int abilityIndex = 0; abilityIndex < abilityDataList.Count; abilityIndex++)
        {
            if (abilityDataList[abilityIndex] == 0)
                continue;

            var abilityName = AbilityService.GetAbilityNameByIndex(abilityIndex);
            attributeInfoList[attributeInfoIndex].SetAttributeText($"{abilityName} + {abilityDataList[abilityIndex]}");
            attributeInfoList[attributeInfoIndex].OnShow();
            ++attributeInfoIndex;
        }

        for (int i = attributeInfoIndex; i < attributeInfoList.Count; i++)
        {
            attributeInfoList[i].OnHide();
        }
    }

    private void SetSellButton()
    {
        sellButton.onClick.AddListener(() =>
        {
            RuneManager.Instance.RemoveRune(uiRune.rune.runeData.Id, isEquippedRune);
            UIManager.Instance.HideAndShowPreview();
        });
    }
}
