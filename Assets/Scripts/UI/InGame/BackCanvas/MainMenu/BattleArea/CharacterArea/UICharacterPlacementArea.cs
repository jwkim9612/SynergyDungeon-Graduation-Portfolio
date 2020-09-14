using System.Collections.Generic;
using UnityEngine;

public class UICharacterPlacementArea : Arranger
{
    public override void Initialize()
    {
        base.Initialize();

        InGameManager.instance.gameState.OnBattle += OnFighting;
        InGameManager.instance.gameState.OnBattle += InitializeCharacterPositions;
        InGameManager.instance.gameState.OnPrepare += OffFighting;
        InGameManager.instance.gameState.OnPrepare += InitializeCharacterPositions;
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

                if(uiCharacters[i].character != null)
                {
                    uiCharacters[i].character.SetSize(CharacterService.SIZE_IN_BATTLE_AREA);
                }
            }
        }
    }

    public void OnFighting()
    {
        foreach(var uiCharacter in uiCharacters)
        {
            uiCharacter.isFightingOnBattlefield = true;
        }
    }

    public void OffFighting()
    {
        foreach (var uiCharacter in uiCharacters)
        {
            uiCharacter.isFightingOnBattlefield = false;
        }
    }

    public List<Character> GetCharacterList()
    {
        List<Character> characters = new List<Character>();

        foreach (var uiCharacter in uiCharacters)
        {
            if(uiCharacter.characterInfo != null)
            {
                characters.Add(uiCharacter.character);
            }
        }

        if(characters.Count == 0)
        {
            return null;
        }
        return characters;
    }

    /// <summary>
    /// 캐릭터가 들어있는 UICharacter리스트를 반환
    /// </summary>
    /// <returns> 캐릭터가 들어있는 UICharacter리스트 </returns>
    public List<UICharacter> GetUICharacterListWithCharacters()
    {
        List<UICharacter> uiCharacters = new List<UICharacter>();

        foreach (var uiCharacter in this.uiCharacters)
        {
            if (uiCharacter.characterInfo != null)
            {
                uiCharacters.Add(uiCharacter);
            }
        }
        return uiCharacters;
    }

    public void ShowAllUICharacters()
    {
        foreach(var uiCharacter in uiCharacters)
        {
            uiCharacter.gameObject.SetActive(true);
        }
    }

    public bool IsEmpty()
    {
        foreach (var uiCharacter in uiCharacters)
        {
            if(uiCharacter.characterInfo != null)
            {
                return false;
            }
        }
        return true;
    }

    public void InitializeCharacterPositions()
    {
        foreach (var uiCharacter in uiCharacters)
        {
            if (uiCharacter.characterInfo == null)
                continue;

            StartCoroutine(uiCharacter.Co_FollowCharacter());
        }
    }
}
