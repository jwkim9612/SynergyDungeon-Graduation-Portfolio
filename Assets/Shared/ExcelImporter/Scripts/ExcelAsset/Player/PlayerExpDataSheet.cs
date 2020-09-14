using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class PlayerExpDataSheet : ScriptableObject, IDataSheet
{
	public List<PlayerExpExcelData> PlayerExpExcelDatas;
    private Dictionary<int, ExpData> PlayerExpDatas;

    public bool TryGetPlayerExpData(int level, out ExpData data)
    {
        data = null;

        if (PlayerExpDatas.TryGetValue(level, out var playerExpData))
        {
            data = new ExpData(playerExpData);
            return true;
        }

        Debug.LogError($"Error TryGetPlayerExpData level:{level}");
        return false;
    }

    public bool TryGetSatisfyExp(int level, out int satisfyExp)
    {
        satisfyExp = 0;

        if (TryGetPlayerExpData(level, out var playerExpData))
        {
            satisfyExp = playerExpData.SatisfyExp;
            return true;
        }

        Debug.LogError($"Error TryGetSatisfyExp level:{level}");
        return false;
    }

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        PlayerExpDatas = new Dictionary<int, ExpData>();

        foreach (var playerExpExcelData in PlayerExpExcelDatas)
        {
            var playerExpData = new ExpData(playerExpExcelData);
            PlayerExpDatas.Add(playerExpData.Level, playerExpData);
        }
    }

    public void DataValidate()
    {
        // 레벨이 고유한 값을 가지는지 확인.
        List<int> levelList = new List<int>();

        foreach (var playerExpExcelData in PlayerExpExcelDatas)
        {
            if (levelList.Contains(playerExpExcelData.Level))
            {
                Debug.Log($"PlayerExp 엑셀 데이터 Level : {playerExpExcelData.Level}값이 겹칩니다.");
            }
            else
            {
                levelList.Add(playerExpExcelData.Level);
            }
        }
    }
}
