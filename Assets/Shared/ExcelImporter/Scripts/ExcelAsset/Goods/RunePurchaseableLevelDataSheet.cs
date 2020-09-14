using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class RunePurchaseableLevelDataSheet : ScriptableObject, IDataSheet
{
	public List<RunePurchaseableLevelExcelData> RunePurchaseableLevelExcelDatas;
    private Dictionary<int, RunePurchaseableLevelData> RunePurchaseableLevelDatas;

	public bool TryGetRunePurchaseableLevel(int salesId, out int level)
	{
		level = 0;

		if (TryGetRunePurchaseableLevelData(salesId, out var runePurchaseableLevelData))
		{
			level = runePurchaseableLevelData.PurchaseableLevel;
			return true;
		}

		Debug.LogError($"Error TryGetRunePurchaseableLevel salesId:{salesId}");
		return false;
	}

	public bool TryGetRunePurchaseableLevelDatas(out Dictionary<int, RunePurchaseableLevelData> runePurchaseableLevelDatas)
	{
		runePurchaseableLevelDatas = new Dictionary<int, RunePurchaseableLevelData>(RunePurchaseableLevelDatas);
		if(runePurchaseableLevelDatas != null)
		{
			return true;
		}

		Debug.LogError("Error TryGetRunePurchaseableLevelDatas");
		return false;
	}

	public bool TryGetRunePurchaseableLevelData(int salesId, out RunePurchaseableLevelData data)
	{
		data = null;

		if(RunePurchaseableLevelDatas.TryGetValue(salesId, out var runePurchaseableLevelData))
		{
			data = new RunePurchaseableLevelData(runePurchaseableLevelData);
			return true;
		}

		Debug.LogError($"Error TryGetRunePurchaseableLevelData salesId:{salesId}");
		return false;
	}

	public void Initialize()
	{
		GenerateData();
	}

	private void GenerateData()
	{
		RunePurchaseableLevelDatas = new Dictionary<int, RunePurchaseableLevelData>();

		foreach (var runePurchaseableLevelExcelData in RunePurchaseableLevelExcelDatas)
		{
			RunePurchaseableLevelData runePurchaseableLevelData = new RunePurchaseableLevelData(runePurchaseableLevelExcelData);
			RunePurchaseableLevelDatas.Add(runePurchaseableLevelData.SalesId, runePurchaseableLevelData);
		}
	}

    public void DataValidate()
    {
        // 룬 구매가능 레벨이 고유한 값을 가지는지 확인.
        List<int> levelList = new List<int>();

        foreach (var runePurchaseableLevelExcelData in RunePurchaseableLevelExcelDatas)
        {
            if (levelList.Contains(runePurchaseableLevelExcelData.PurchaseableLevel))
            {
                Debug.Log($"RunePurchaseableLevel 엑셀 데이터 PurchaseableLevel : {runePurchaseableLevelExcelData.PurchaseableLevel}값이 겹칩니다.");
			}
			else
			{
				levelList.Add(runePurchaseableLevelExcelData.PurchaseableLevel);
			}
		}
    }
}
