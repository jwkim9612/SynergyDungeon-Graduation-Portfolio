using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class TribeDataSheet : ScriptableObject, IDataSheet
{
	public List<TribeExcelData> TribeExcelDatas;
	private Dictionary<Tribe, TribeData> TribeDatas;

	public bool TryGetTribeData(Tribe tribe, out TribeData tribeData)
	{
		tribeData = new TribeData(TribeDatas[tribe]);
		if (tribeData != null)
		{
			return true;
		}

		Debug.LogError("Error TryGetTribeData");
		return false;
	}

	public bool TryGetTribeDatas(out Dictionary<Tribe, TribeData> tribeDatas)
	{
		tribeDatas = new Dictionary<Tribe, TribeData>(TribeDatas);
		if (tribeDatas != null)
		{
			return true;
		}

		Debug.LogError("Error TryGetTribeDatas");
		return false;
	}


	public bool TryGetTribeImage(Tribe tribe, out Sprite sprite)
	{
		sprite = null;

		if (TribeDatas.TryGetValue(tribe, out var tribeData))
		{
			sprite = tribeData.Image;
			return true;
		}

		Debug.LogError("Error TryGetTribeImage");
		return false;
	}

	public void Initialize()
	{
		GenerateData();
	}

	private void GenerateData()
	{
		TribeDatas = new Dictionary<Tribe, TribeData>();

		foreach (var tribeExcelData in TribeExcelDatas)
		{
			TribeData tribeData = new TribeData(tribeExcelData);
			TribeDatas.Add(tribeData.Tribe, tribeData);
		}
	}

    public void DataValidate()
    {
        // Tribe가 고유한 값을 가지는지 확인.
        List<Tribe> tribeList = new List<Tribe>();

        foreach (var tribeExcelData in TribeExcelDatas)
        {
            if (tribeList.Contains(tribeExcelData.Name))
            {
                Debug.Log($"Tribe 엑셀 데이터 Tribe : {tribeExcelData.Name}값이 겹칩니다.");
            }
			else
			{
				tribeList.Add(tribeExcelData.Name);
			}
		}
    }
}
