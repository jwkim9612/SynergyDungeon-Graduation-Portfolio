using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class RuneDataSheet : ScriptableObject, IDataSheet
{
	public List<RuneExcelData> RuneExcelDatas;
    private Dictionary<int, RuneData> RuneDatas;

    public bool TryGetRuneDatas(out Dictionary<int, RuneData> runeDatas)
    {
        runeDatas = new Dictionary<int, RuneData>(RuneDatas);
        if(runeDatas != null)
        {
            return true;
        }

        Debug.LogError("Error TryGetRuneDatas");
        return false;
    }

    public bool TryGetRuneData(int runeId, out RuneData data)
    {
        data = null;

        if(RuneDatas.TryGetValue(runeId, out var runeData))
        {
            data = new RuneData(runeData);
            return true;
        }

        Debug.LogError($"Error TryGetRuneData runeId:{runeId}");
        return false;
    }

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        RuneDatas = new Dictionary<int, RuneData>();

        foreach (var runeExcelData in RuneExcelDatas)
        {
            RuneData runeData = new RuneData(runeExcelData);
            RuneDatas.Add(runeData.Id, runeData);
        }
    }

    public void DataValidate()
    {
        // 아이디가 고유한 값을 가지는지 확인.
        List<int> idList = new List<int>();

        foreach (var runeExcelData in RuneExcelDatas)
        {
            if (idList.Contains(runeExcelData.Id))
            {
                Debug.Log($"Rune 엑셀 데이터 Grade : {runeExcelData.Id}값이 겹칩니다.");
            }
            else
            {
                idList.Add(runeExcelData.Id);
            }
        }
    }
}
