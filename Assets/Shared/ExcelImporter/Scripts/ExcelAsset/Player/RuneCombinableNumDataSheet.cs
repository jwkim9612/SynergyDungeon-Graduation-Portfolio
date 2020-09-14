using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class RuneCombinableNumDataSheet : ScriptableObject, IDataSheet
{
	public List<RuneCombinableNumExcelData> RuneCombinableNumExcelDatas;
    private Dictionary<RuneGrade, int> RuneCombinableNumDatas;

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        RuneCombinableNumDatas = new Dictionary<RuneGrade, int>();

        foreach (var runeCombinableNumExcelData in RuneCombinableNumExcelDatas)
        {
            RuneGrade grade = runeCombinableNumExcelData.Grade;
            int num = runeCombinableNumExcelData.Num;

            RuneCombinableNumDatas.Add(grade, num);
        }
    }

    public bool TryGetRuneCombinableNum(RuneGrade grade, out int num)
    {
        num = 0;

        if (RuneCombinableNumDatas.TryGetValue(grade, out var runeCombinableNum))
        {
            num = runeCombinableNum;
            return true;
        }

        Debug.LogError($"Error TryGetRuneCombinableNum grade:{grade}");
        return false;
    }

    public void DataValidate()
    {
        // 등급이 고유한 값을 가지는지 확인.
        List<RuneGrade> gradeList = new List<RuneGrade>();

        foreach (var runeCombinableNumExcelData in RuneCombinableNumExcelDatas)
        {
            if (gradeList.Contains(runeCombinableNumExcelData.Grade))
            {
                Debug.Log($"RuneCombinableNum 엑셀 데이터 Grade : {runeCombinableNumExcelData.Grade}값이 겹칩니다.");
            }
            else
            {
                gradeList.Add(runeCombinableNumExcelData.Grade);
            }
        }
    }
}
