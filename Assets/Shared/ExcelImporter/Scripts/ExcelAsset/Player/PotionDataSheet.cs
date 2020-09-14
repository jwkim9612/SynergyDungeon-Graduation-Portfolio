using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class PotionDataSheet : ScriptableObject, IDataSheet
{
    public List<PotionExcelData> PotionExcelDatas;
    private Dictionary<int, PotionData> PotionDatas;

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        PotionDatas = new Dictionary<int, PotionData>();

        foreach (var potionExcelData in PotionExcelDatas)
        {
            PotionData potionData = new PotionData(potionExcelData);
            PotionDatas.Add(potionData.Id, potionData);
        }
    }

    public bool TryGetPotionImage(int potionId, out Sprite sprite)
    {
        sprite = null;

        if (TryGetPotionData(potionId, out var potionData))
        {
            sprite = potionData.Image;
            return true;
        }

        Debug.LogError($"Error TryGetPotionImage potionId:{potionId}");
        return false;
    }

    public bool TryGetPotionData(int potionId, out PotionData data)
    {
        data = null;

        if (PotionDatas.TryGetValue(potionId, out var potionData))
        {
            data = new PotionData(potionData);
            return true;
        }

        Debug.LogError($"Error TryGetPotionData potionId:{potionId}");
        return false;
    }

    public bool TryGetPotionDatas(out Dictionary<int, PotionData> potionDatas)
    {
        potionDatas = new Dictionary<int, PotionData>(PotionDatas);
        if (potionDatas != null)
        {
            return true;
        }

        Debug.LogError("Error TryGetPotionDatas");
        return false;
    }

    public void DataValidate()
    {
        // 아이디가 고유한 값을 가지는지 확인.
        List<int> idList = new List<int>();

        foreach (var potionExcelData in PotionExcelDatas)
        {
            if (idList.Contains(potionExcelData.Id))
            {
                Debug.Log($"Potion 엑셀 데이터 Id : {potionExcelData.Id}값이 겹칩니다.");
            }
            else
            {
                idList.Add(potionExcelData.Id);
            }
        }
    }
}
