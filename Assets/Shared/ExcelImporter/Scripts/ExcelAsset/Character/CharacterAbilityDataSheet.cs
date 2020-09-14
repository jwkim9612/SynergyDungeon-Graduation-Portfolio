using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class CharacterAbilityDataSheet : ScriptableObject, IDataSheet
{
	public List<CharacterAbilityExcelData> OneStarExcelDatas;
    public List<CharacterAbilityExcelData> TwoStarExcelDatas;
    public List<CharacterAbilityExcelData> ThreeStarExcelDatas;
    private Dictionary<int, CharacterAbilityData> OneStarDatas;
    private Dictionary<int, CharacterAbilityData> TwoStarDatas;
    private Dictionary<int, CharacterAbilityData> ThreeStarDatas;

    public void Initialize()
    {
        GenerateData();
    }

    public bool TryGetCharacterAbilityData(int characterId, int star, out CharacterAbilityData data)
    {
        data = null;

        switch (star)
        {
            case 1:
                if (OneStarDatas.TryGetValue(characterId, out data))
                {
                    return true;
                }
                break;
            case 2:
                if (TwoStarDatas.TryGetValue(characterId, out data))
                {
                    return true;
                }
                break;
            case 3:
                if (ThreeStarDatas.TryGetValue(characterId, out data))
                {
                    return true;
                }
                break;
        }

        Debug.LogError($"Error TryGetCharacterAbilityData characterId:{characterId}, star:{star}");
        return false;
    }

    private void GenerateData()
    {
        OneStarDatas = new Dictionary<int, CharacterAbilityData>();
        TwoStarDatas = new Dictionary<int, CharacterAbilityData>();
        ThreeStarDatas = new Dictionary<int, CharacterAbilityData>();

        foreach (var oneStarExcelData in OneStarExcelDatas)
        {
            CharacterAbilityData characterAbilityData = new CharacterAbilityData(oneStarExcelData);
            OneStarDatas.Add(characterAbilityData.Id, characterAbilityData);
        }

        foreach (var twoStarExcelData in TwoStarExcelDatas)
        {
            CharacterAbilityData characterAbilityData = new CharacterAbilityData(twoStarExcelData);
            TwoStarDatas.Add(characterAbilityData.Id, characterAbilityData);
        }

        foreach (var threeStarExcelData in ThreeStarExcelDatas)
        {
            CharacterAbilityData characterAbilityData = new CharacterAbilityData(threeStarExcelData);
            ThreeStarDatas.Add(characterAbilityData.Id, characterAbilityData);
        }
    }

    public void DataValidate()
    {
        // 아이디가 고유한 값을 가지는지 확인
        List<int> idList = new List<int>();

        foreach (var oneStarExcelData in OneStarExcelDatas)
        {
            if (idList.Contains(oneStarExcelData.Id))
            {
                Debug.Log($"CharacterAbility 엑셀 데이터 OneStarData의 Id : {oneStarExcelData.Id}값이 겹칩니다.");
            }
            else
            {
                idList.Add(oneStarExcelData.Id);
            }
        }

        idList = new List<int>();

        foreach (var twoStarExcelData in TwoStarExcelDatas)
        {
            if (idList.Contains(twoStarExcelData.Id))
            {
                Debug.Log($"CharacterAbility 엑셀 데이터 TwoStarData Id : {twoStarExcelData.Id}값이 겹칩니다.");
            }
            else
            {
                idList.Add(twoStarExcelData.Id);
            }
        }

        idList = new List<int>();

        foreach (var threeStarExcelData in ThreeStarExcelDatas)
        {
            if (idList.Contains(threeStarExcelData.Id))
            {
                Debug.Log($"CharacterAbility 엑셀 데이터 ThreeStarData의 Id : {threeStarExcelData.Id}값이 겹칩니다.");
            }
            else
            {
                idList.Add(threeStarExcelData.Id);
            }
        }
    }
}
