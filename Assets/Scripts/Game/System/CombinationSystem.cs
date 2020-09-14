using System.Collections.Generic;
using UnityEngine;

// 인게임 캐릭터 조합 시스템(별 업그레이드)
public class CombinationSystem
{
    private Dictionary<CharacterInfo, int> characterList;

    public CombinationSystem()
    {
        characterList = new Dictionary<CharacterInfo, int>();
    }

    public void Initialize()
    {

    }
   
    // 캐릭터 리스트에 캐릭터 추가
    public void AddCharacter(CharacterInfo characterInfo)
    { 
        if (characterList.ContainsKey(characterInfo))
        {
            ++characterList[characterInfo];
        }
        else
        {
            characterList.Add(characterInfo, 1);
        }

        // 3개가 모였으면
        if(characterList[characterInfo] == CharacterService.NUMBER_REQUIRED_FOR_COMBINATION)
        {
            InGameManager.instance.draggableCentral.CombinationCharacter(characterInfo);
            characterList.Remove(characterInfo);
        }
    }

    // 캐릭터 아이디를 통해 캐릭터리스트에 추가
    public void AddCharacter(int characterId)
    {
        CharacterInfo characterInfo = new CharacterInfo(characterId);
        AddCharacter(characterInfo);
    }

    // 캐릭터 리스트에서 캐릭터 빼기
    public void SubCharacter(CharacterInfo characterInfo)
    {
        if (characterList.ContainsKey(characterInfo))
        {
            --characterList[characterInfo];
        }
        else
        {
            Debug.Log("Error No Character");
        }
    }

    // 캐릭터 리스트에 해당 캐릭터가 2개 있는지에 대한 결과 반환
    public bool IsUpgradable(CharacterInfo characterInfo)
    {
        // 해당 키가 있고
        if(characterList.ContainsKey(characterInfo))
        {
            // 2개가 있으면
            if(characterList[characterInfo] == CharacterService.NUMBER_REQUIRED_FOR_COMBINATION - 1)
            {
                return true;
            }
        }

        return false;
    }
}
