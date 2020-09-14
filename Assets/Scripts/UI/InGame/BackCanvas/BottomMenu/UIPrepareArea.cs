using System.Collections.Generic;

public class UIPrepareArea : Arranger
{
    public override void Initialize()
    {
        base.Initialize();
        
        if(SaveManager.Instance.IsLoadedData)
        {
            var prepareAreaInfoList = SaveManager.Instance.inGameSaveData.PrepareAreaInfoList;

            SetUICharacterList(prepareAreaInfoList);
        }

        InGameManager.instance.gameState.OnBattle += HideAllCharacters;
        InGameManager.instance.gameState.OnPrepare += ShowAllCharacters;
    }

    public override void UpdateChildren()
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

                if (uiCharacters[i].character != null)
                {
                    uiCharacters[i].character.SetSize(CharacterService.SIZE_IN_PREPARE_AREA);
                }
            }
        }
    }

    public UICharacter GetEmptyUICharacter()
    {
        foreach(var uiCharacter in uiCharacters)
        {
            if (uiCharacter.characterInfo != null)
                continue;

            return uiCharacter;
        }

        return null;
    }

    public List<UICharacter> GetUICharacterListWithCharacters()
    {
        List<UICharacter> characterList = new List<UICharacter>();

        foreach (var uiCharacter in uiCharacters)
        {
            if (uiCharacter.characterInfo != null)
                characterList.Add(uiCharacter);
        }

        return characterList;
    }

    public void HideAllCharacters()
    {
        foreach (var uiCharacter in uiCharacters)
        {
            if (uiCharacter.characterInfo == null)
                continue;

            uiCharacter.character.OnHide();
        }
    }

    public void ShowAllCharacters()
    {
        foreach (var uiCharacter in uiCharacters)
        {
            if (uiCharacter.characterInfo == null)
                continue;

            uiCharacter.character.OnShow();
        }
    }
}
