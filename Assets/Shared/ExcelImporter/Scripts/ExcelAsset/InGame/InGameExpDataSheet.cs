using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class InGameExpDataSheet : ScriptableObject, IDataSheet
{
	public List<InGameExpExcelData> InGameExpExcelDatas;
    private Dictionary<int, ExpData> InGameExpDatas;

    public bool TryGetInGameExpData(int level, out ExpData data)
    {
        data = null;

        if (InGameExpDatas.TryGetValue(level, out var inGameExpData))
        {
            data = new ExpData(inGameExpData);
            return true;
        }

        Debug.LogError($"Error TryGetInGameExpData level:{level}");
        return false;
    }

    public bool TryGetSatisfyExp(int level, out int satisfyExp)
    {
        satisfyExp = 0;

        if(TryGetInGameExpData(level, out var inGameExpData))
        {
            satisfyExp = inGameExpData.SatisfyExp;
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
        InGameExpDatas = new Dictionary<int, ExpData>();

        foreach (var inGameExpExcelData in InGameExpExcelDatas)
        {
            var inGameExpData = new ExpData(inGameExpExcelData);
            InGameExpDatas.Add(inGameExpData.Level, inGameExpData);
        }
    }

    public void DataValidate()
    {
        // ������ ������ ���� �������� Ȯ��.
        List<int> levelList = new List<int>();

        foreach (var inGameExpExcelData in InGameExpExcelDatas)
        {
            if (levelList.Contains(inGameExpExcelData.Level))
            {
                Debug.Log($"InGameExp ���� ������ Level : {inGameExpExcelData.Level}���� ��Ĩ�ϴ�.");
            }
            else
            {
                levelList.Add(inGameExpExcelData.Level);
            }
        }
    }
}
