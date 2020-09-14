using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class CharacterDataSheet : ScriptableObject, IDataSheet
{
	public List<CharacterExcelData> CharacterExcelDatas;
	private Dictionary<int, CharacterData> CharacterDatas;

	public bool TryGetCharacterDatas(out Dictionary<int, CharacterData> characterDatas)
	{
		characterDatas = new Dictionary<int, CharacterData>(CharacterDatas);
		if(characterDatas != null)
		{
			return true;
		}

		Debug.LogError("Error TryGetCharacterDatas");
		return false;
	}

	public bool TryGetCharacterRunTimeAnimatorController(int characterId, out RuntimeAnimatorController runTimeAnimatorController)
	{
		runTimeAnimatorController = null;

		if (TryGetCharacterData(characterId, out var characterData))
		{
			runTimeAnimatorController = characterData.RuntimeAnimatorController;
			return true;
		}

		Debug.LogError($"Error TryGetCharacterRunTimeAnimatorController characterId:{characterId}");
		return false;
	}

	public bool TryGetCharacterImage(int characterId, out Sprite sprite)
	{
		sprite = null;

		if (TryGetCharacterData(characterId, out var characterData))
		{
			sprite = characterData.Image;
			return true;
		}

		Debug.LogError($"Error TryGetCharacterImage characterId:{characterId}");
		return false;
	}

    public bool TryGetCharacterHeadImage(int characterId, out Sprite sprite)
    {
        sprite = null;

        if (TryGetCharacterData(characterId, out var characterData))
        {
            sprite = characterData.HeadImage;
            return true;
        }

        Debug.LogError($"Error TryGetCharacterHeadImage characterId:{characterId}");
        return false;
    }

    public bool TryGetCharacterName(int characterId, out string name)
	{
		name = "";

		if (TryGetCharacterData(characterId, out var characterData))
		{
			name = characterData.Name;
			return true;
		}

		Debug.LogError($"Error TryGetCharacterName characterId:{characterId}");
		return false;
	}

	public bool TryGetCharacterTier(int characterId, out Tier tier)
	{
		tier = Tier.None;

		if (TryGetCharacterData(characterId, out var characterData))
		{
			tier = characterData.Tier;
			return true;
		}

		Debug.LogError($"Error TryGetCharacterTier characterId:{characterId}");
		return false;
	}

	public bool TryGetCharacterOrigin(int characterId, out Origin origin)
	{
		origin = Origin.None;

		if (TryGetCharacterData(characterId, out var characterData))
		{
			origin = characterData.Origin;
			return true;
		}

		Debug.LogError($"Error TryGetCharacterOrigin characterId:{characterId}");
		return false;
	}

	public bool TryGetCharacterTribe(int characterId, out Tribe tribe)
	{
		tribe = Tribe.None;

		if (TryGetCharacterData(characterId, out var characterData))
		{
			tribe = characterData.Tribe;
			return true;
		}

		Debug.LogError($"Error TryGetCharacterTribe characterId:{characterId}");
		return false;
	}

	public bool TryGetCharacterData(int characterId, out CharacterData data)
	{
		data = null;

		if(CharacterDatas.TryGetValue(characterId, out var characterData))
		{
			data = new CharacterData(characterData);
			return true;
		}

		Debug.LogError($"Error TryGetCharacterData characterId:{characterId}");
		return false;
	}

	public List<CharacterData> GetCharacterDataListByTribe(Tribe tribe)
	{
		List<CharacterData> characterDataList = new List<CharacterData>();

		foreach(var characterData in CharacterDatas)
		{
			if(characterData.Value.Tribe == tribe)
			{
				characterDataList.Add(characterData.Value);
			}
		}

		return characterDataList;
	}

	public List<CharacterData> GetCharacterDataListByOrigin(Origin origin)
	{
		List<CharacterData> characterDataList = new List<CharacterData>();

		foreach (var characterData in CharacterDatas)
		{
			if (characterData.Value.Origin == origin)
			{
				characterDataList.Add(characterData.Value);
			}
		}

		return characterDataList;
	}

	public void Initialize()
	{
		GenerateData();
	}

	private void GenerateData()
	{
		CharacterDatas = new Dictionary<int, CharacterData>();

        foreach (var characterExcelData in CharacterExcelDatas)
        {
			CharacterData characterData = new CharacterData(characterExcelData);
			CharacterDatas.Add(characterData.Id, characterData);
        }
    }

    public void DataValidate()
    {
        // 아이디가 고유한 값을 가지는지 확인.
        List<int> idList = new List<int>();

        foreach (var characterExcelData in CharacterExcelDatas)
        {
            if (idList.Contains(characterExcelData.Id))
            {
                Debug.Log($"Character 엑셀 데이터 Id : {characterExcelData.Id}값이 겹칩니다.");
			}
			else
			{
				idList.Add(characterExcelData.Id);
			}
		}
    }
}
