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
        // ������ ������ ���� �������� Ȯ��.
        List<int> levelList = new List<int>();

        foreach (var playerExpExcelData in PlayerExpExcelDatas)
        {
            if (levelList.Contains(playerExpExcelData.Level))
            {
                Debug.Log($"PlayerExp ���� ������ Level : {playerExpExcelData.Level}���� ��Ĩ�ϴ�.");
            }
            else
            {
                levelList.Add(playerExpExcelData.Level);
            }
        }
    }
}
