using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameSynergyInfo : MonoBehaviour
{
    [SerializeField] private Image synergyImage = null;
    [SerializeField] private Text synergyNameText = null;
    [SerializeField] private Text synergyInfoText = null;
    [SerializeField] private List<UIInGameSynergyInfoCharacter> characterImageList = null;

    [SerializeField] private GameObject inBattleParent = null;
    [SerializeField] private GameObject inPrepareParent = null;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += MoveForBattle;
        InGameManager.instance.gameState.OnPrepare += MoveForPrepare;

        characterImageList = GetComponentsInChildren<UIInGameSynergyInfoCharacter>().ToList();
    }

    private void MoveForBattle()
    {
        TransformService.SetParentAndMoveRelativeToParent(this.transform, inBattleParent);
    }

    private void MoveForPrepare()
    {
        TransformService.SetParentAndMoveRelativeToParent(this.transform, inPrepareParent);
    }

    public void SetSynergyInfo(Tribe tribe)
    {
        var tribeDataSheet = DataBase.Instance.tribeDataSheet;
        if(tribeDataSheet.TryGetTribeData(tribe, out var tribeData))
        {
            SetSynergyImage(tribeData.Image);
            SetSynergyNameText(SynergyService.GetNameByTribe(tribeData.Tribe));
            SetSynergyInfoText(tribeData.Description);
        }

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        var characterDataList = characterDataSheet.GetCharacterDataListByTribe(tribe);
        int index = 0;
        foreach(var characterData in characterDataList)
        {
            characterImageList[index].SetCharacterImage(characterData.Image);
            characterImageList[index].Disabled();

            var characterList = InGameManager.instance.draggableCentral.uiCharacterArea.GetCharacterList();
            foreach (var character in characterList)
            {
                if (character.characterInfo.id == characterData.Id)
                {
                    characterImageList[index].Activate();
                    break;
                }
            }

            characterImageList[index].OnShow();

            ++index;
        }

        for(int i = index; i < characterImageList.Count; ++i)
        {
            characterImageList[i].OnHide();
        }
    }

    public void SetSynergyInfo(Origin origin)
    {
        var originDataSheet = DataBase.Instance.originDataSheet;
        if (originDataSheet.TryGetOriginData(origin, out var originData))
        {
            SetSynergyImage(originData.Image);
            SetSynergyNameText(SynergyService.GetNameByOrigin(originData.Origin));
            SetSynergyInfoText(originData.Description);
        }

        var characterDataSheet = DataBase.Instance.characterDataSheet;
        var characterDataList = characterDataSheet.GetCharacterDataListByOrigin(origin);
        int index = 0;
        foreach (var characterData in characterDataList)
        {
            characterImageList[index].SetCharacterImage(characterData.Image);
            characterImageList[index].Disabled();

            var characterList = InGameManager.instance.draggableCentral.uiCharacterArea.GetCharacterList();
            foreach(var character in characterList)
            {
                if(character.characterInfo.id == characterData.Id)
                {
                    characterImageList[index].Activate();
                    break;
                }
            }

            characterImageList[index].OnShow();

            ++index;
        }

        for (int i = index; i < characterImageList.Count; ++i)
        {
            characterImageList[i].OnHide();
        }
    }

    private void SetSynergyImage(Sprite sprite)
    {
        synergyImage.sprite = sprite;
    }

    private void SetSynergyNameText(string synergyName)
    {
        synergyNameText.text = synergyName;
    }

    private void SetSynergyInfoText(string synergyInfo)
    {
        synergyInfoText.text = synergyInfo;
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
