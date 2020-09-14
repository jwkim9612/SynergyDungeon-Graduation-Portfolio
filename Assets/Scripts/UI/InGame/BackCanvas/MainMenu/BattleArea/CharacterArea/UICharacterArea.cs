using System.Collections.Generic;
using UnityEngine;

public class UICharacterArea : MonoBehaviour
{
    public delegate void OnPlacementChangedDelegate();
    public OnPlacementChangedDelegate OnPlacementChanged { get; set; }

    public UICharacterPlacementArea frontArea;
    public UICharacterPlacementArea backArea;
    public int numOfCurrentPlacedCharacters { get; set; }

    public void Initialize()
    {
        frontArea.Initialize();
        backArea.Initialize();

        SetData();

        InGameManager.instance.gameState.OnComplete += ShowAllUICharacters;
    }

    private void SetData()
    {
        if (SaveManager.Instance.IsLoadedData)
        {
            var characterAreaInfoList = SaveManager.Instance.inGameSaveData.CharacterAreaInfoList;

            SetUICharacterList(characterAreaInfoList);
            OnPlacementChanged?.Invoke();
        }
        else
        {
            numOfCurrentPlacedCharacters = 0;
        }
    }

    public bool IsEmpty()
    {
        return backArea.IsEmpty() && frontArea.IsEmpty() ? true : false;
    }

    public List<CharacterInfo> GetAllCharacterInfo()
    {
        List<CharacterInfo> characterInfoList = new List<CharacterInfo>();

        foreach (var uiCharacter in backArea.uiCharacters)
        {
            characterInfoList.Add(uiCharacter.characterInfo);
        }

        foreach (var uiCharacter in frontArea.uiCharacters)
        {
            characterInfoList.Add(uiCharacter.characterInfo);
        }

        return characterInfoList;
    }

    protected void SetUICharacterList(List<CharacterInfo> characterInfoList)
    {
        if (characterInfoList.Count == 0)
            return;

        List<CharacterInfo> backAreaInfos = characterInfoList.GetRange(0, InGameService.NUMBER_OF_BACKAREA);
        List<CharacterInfo> frontAreaInfos = characterInfoList.GetRange(InGameService.NUMBER_OF_BACKAREA, InGameService.NUMBER_OF_FRONTAREA);

        backArea.SetUICharacterList(backAreaInfos);
        frontArea.SetUICharacterList(frontAreaInfos);

        var uiCharacterListWithCharacter = GetUICharacterListWithCharacters();
        numOfCurrentPlacedCharacters = uiCharacterListWithCharacter.Count;

        foreach (var uiCharacter in uiCharacterListWithCharacter)
        {
            uiCharacter.character.SetSize(CharacterService.SIZE_IN_BATTLE_AREA);
            uiCharacter.SetAnimationImage();
        }
    }

    public void ShowAllUICharacters()
    {
        backArea.ShowAllUICharacters();
        frontArea.ShowAllUICharacters();
    }

    public List<Character> GetCharacterList()
    {
        List<Character> characters = new List<Character>();

        var backAreaCharacterList = backArea.GetCharacterList();
        var frontAreaCharacterList = frontArea.GetCharacterList();

        if (backAreaCharacterList != null)
        {
            characters.AddRange(backArea.GetCharacterList());
        }

        if (frontAreaCharacterList != null)
        {
            characters.AddRange(frontArea.GetCharacterList());
        }

        if (characters.Count == 0)
        {
            return null;
        }

        return characters;
    }

    public void ResetAllCharacterAbility()
    {
        var characterList = GetCharacterList();
        if (characterList == null)
            return;

        foreach(var character in characterList)
        {
            character.ResetAbility();
        }
    }

    /// <summary>
    /// 캐릭터가 있는 UICharacter들을 반환
    /// </summary>
    /// <returns></returns>
    public List<UICharacter> GetUICharacterListWithCharacters()
    {
        List<UICharacter> uiCharacters = new List<UICharacter>();

        uiCharacters.AddRange(backArea.GetUICharacterListWithCharacters());
        uiCharacters.AddRange(frontArea.GetUICharacterListWithCharacters());

        return uiCharacters;
    }

    /// <summary>
    /// 배틀 공간에 배치한 캐릭터 수 1증가
    /// </summary>
    public void AddCurrentPlacedCharacter()
    {
        if (numOfCurrentPlacedCharacters == InGameService.MAX_NUMBER_OF_CAN_PLACED)
            return;

        ++numOfCurrentPlacedCharacters;
        OnPlacementChanged();
    }

    /// <summary>
    /// 배틀 공간에 배치한 캐릭터 수 1감소
    /// </summary>
    public void SubCurrentPlacedCharacter()
    {
        if (numOfCurrentPlacedCharacters == InGameService.MIN_NUMBER_OF_CAN_PLACED)
            return;

        --numOfCurrentPlacedCharacters;
        OnPlacementChanged();
    }

    /// <summary>
    /// 조합으로 인한 배틀 공간에 배치한 캐릭터 수 1감소
    /// </summary>
    /// <param name="uiCharacter"></param>
    /// <param name="isFirstCharacter"></param>
    public void SubCurrentPlacedCharacterFromCombinations(UICharacter uiCharacter, bool isFirstCharacter)
    {
        if (isFirstCharacter)
            return;

        if (uiCharacter.GetArea<UIBattleArea>() != null)
        {
            SubCurrentPlacedCharacter();
        }
    }
}
