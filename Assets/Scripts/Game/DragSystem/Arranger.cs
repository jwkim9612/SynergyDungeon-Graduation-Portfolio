using System.Collections.Generic;
using UnityEngine;

// 인게임에서 캐릭터들이 존재하는 공간 (드래그 시스템에서 사용)
public class Arranger : MonoBehaviour
{
    public List<UICharacter> uiCharacters { get; set; }

    public virtual void Initialize()
    {
        uiCharacters = new List<UICharacter>();

        UpdateChildren();
        InitializeUICharacters();
    }

    // 리스트에 있는 캐릭터들을 초기화
    private void InitializeUICharacters()
    {
        foreach (var uiCharacter in uiCharacters)
        {
            uiCharacter.Initialize();
        }
    }

    // UI캐릭터 리스트를 업데이트
    public virtual void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (i == uiCharacters.Count)
            {
                uiCharacters.Add(null);
            }

            // border안에 또 캐릭터가 있어서 GetChild를 두 번 써줌
            var uicharacter = gameObject.GetComponentsInChildren<UISlot>()[i].GetComponentInChildren<UICharacter>();

            if (uicharacter != uiCharacters[i])
            {
                uiCharacters[i] = uicharacter;
            }
        }
    }

    // 파라미터로 받은 캐릭터의 (드래그한)위치가 캐릭터리스트에 있는 캐릭터들 중 하나의 위치에 오면 그 위치에 있는 캐릭터를 반환
    public UICharacter GetCharacterByPosition(UICharacter draggedUICharacter)
    {
        UICharacter targetCharacter = null;

        for(int i = 0; i < uiCharacters.Count; ++i)
        { 
            if(TransformService.ContainPos(uiCharacters[i].transform as RectTransform, draggedUICharacter.transform.position))
            {
                targetCharacter = uiCharacters[i];
                break;
            }
        }

        return targetCharacter;
    }

    // 현재 Arranger에있는 캐릭터들의 정보리스트를 반환
    public List<CharacterInfo> GetAllCharacterInfo()
    {
        List<CharacterInfo> characterInfoList = new List<CharacterInfo>();

        foreach (var uiCharacter in uiCharacters)
        {
            characterInfoList.Add(uiCharacter.characterInfo);
        }

        return characterInfoList;
    }

    // 캐릭터 정보 리스트로 UI캐릭터 리스트를 셋팅
    public void SetUICharacterList(List<CharacterInfo> characterInfoList)
    {
        if (characterInfoList.Count == 0)
            return;

        for (int i = 0; i < uiCharacters.Count; ++i)
        {
            if (characterInfoList[i] == null)
                continue;

            InGameManager.instance.characterStockSystem.RemoveStockId(characterInfoList[i]);
            InGameManager.instance.combinationSystem.AddCharacter(characterInfoList[i]);

            var uiCharacter = uiCharacters[i];
            uiCharacter.SetCharacter(characterInfoList[i]);

            if (uiCharacter.GetArea<UICharacterArea>() != null)
            {
                uiCharacter.character.SetSize(CharacterService.SIZE_IN_BATTLE_AREA);
                uiCharacter.SetAnimationImage();
                uiCharacter.character.isOnBattlefield = true;
            }
            else if (uiCharacter.GetArea<UIPrepareArea>() != null)
            {
                uiCharacter.character.SetSize(CharacterService.SIZE_IN_PREPARE_AREA);
            }
        }
    }

    //public void SpaceExpansion()
    //{
    //    RectTransform rect = transform as RectTransform;
    //    //rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + InGameService.SIZE_TO_EXPAND_THE_BATTLE_AREA);

    //}

    //public void SpaceReduction()
    //{
    //    RectTransform rect = transform as RectTransform;
    //    //rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - InGameService.SIZE_TO_EXPAND_THE_BATTLE_AREA);
    //}
}
