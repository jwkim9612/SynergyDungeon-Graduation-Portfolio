using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ChapterInfoDataSheet : ScriptableObject, IDataSheet
{
	public List<ChapterInfoExcelData> Chapter1InfoExcelDatas;
	public List<ChapterInfoExcelData> Chapter2InfoExcelDatas;

	//private Dictionary<int, ChapterInfoData> ChapterInfoDatas;
	private Dictionary<int, Dictionary<int, ChapterInfoData>> AllChapterInfoDatas;

	public bool TryGetChapterInfoExpAmount(int chapterId, int waveId, out int amount)
	{
		amount = 0;

		if (TryGetChapterInfoData(chapterId, waveId, out var chapterInfoData))
		{
			amount = chapterInfoData.ExpAmount;
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoExpAmount chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoGoldAmount(int chapterId, int waveId, out int amount)
	{
		amount = 0;

		if(TryGetChapterInfoData(chapterId, waveId, out var chapterInfoData))
		{
			amount = chapterInfoData.GoldAmount;
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoGoldAmount chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoClearGoldReward(int chapterId, int waveId, out int rewardValue)
	{
		rewardValue = 0;

		if (TryGetChapterInfoData(chapterId, waveId, out var chapterInfoData))
		{
			rewardValue = chapterInfoData.ClearGoldReward;
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoClearGoldReward chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoClearExpReward(int chapterId, int waveId, out int rewardValue)
	{
		rewardValue = 0;

		if (TryGetChapterInfoData(chapterId, waveId, out var chapterInfoData))
		{
			rewardValue = chapterInfoData.ClearExpReward;
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoClearExpReward chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoData(int chapterId, int waveId, out ChapterInfoData data)
	{
		data = null;

		if (TryGetChapterInfoDatas(chapterId, out var chapterInfoDatas))
		{
			if(chapterInfoDatas.TryGetValue(waveId, out data))
			{
				return true;
			}
		}

		Debug.LogWarning($"Error TryGetChapterInfoData chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoDatas(int chapterId, out Dictionary<int, ChapterInfoData> datas)
	{
		if (AllChapterInfoDatas.TryGetValue(chapterId, out datas))
		{
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoDatas chapterId:{chapterId}");
		return false;
	}

	public void Initialize()
	{
		AllChapterInfoDatas = new Dictionary<int, Dictionary<int, ChapterInfoData>>();

		GenerateData();
	}

	private void GenerateData()
	{
		GenerateChapter1InfoData();
		GenerateChapter2InfoData();
	}

	private void GenerateChapter1InfoData()
	{
        Dictionary<int, ChapterInfoData> ChapterInfoDatas = new Dictionary<int, ChapterInfoData>();

        foreach (var chapterInfoExcelData in Chapter1InfoExcelDatas)
        {
            ChapterInfoData chapterInfoData = new ChapterInfoData(chapterInfoExcelData);
            ChapterInfoDatas.Add(chapterInfoData.WaveId, chapterInfoData);
        }

        AllChapterInfoDatas.Add(Chapter1InfoExcelDatas[0].ChapterId, ChapterInfoDatas);
    }

    private void GenerateChapter2InfoData()
    {
        Dictionary<int, ChapterInfoData> ChapterInfoDatas = new Dictionary<int, ChapterInfoData>();

        foreach (var chapterInfoExcelData in Chapter2InfoExcelDatas)
        {
            ChapterInfoData chapterInfoData = new ChapterInfoData(chapterInfoExcelData);
            ChapterInfoDatas.Add(chapterInfoData.WaveId, chapterInfoData);
        }

        AllChapterInfoDatas.Add(Chapter2InfoExcelDatas[0].ChapterId, ChapterInfoDatas);
    }

    public void DataValidate()
    {
		// 아이디가 고유한 값을 가지는지 확인.
		List<int> idList = new List<int>();

        foreach (var chapterInfoExcelData in Chapter1InfoExcelDatas)
        {
            if (idList.Contains(chapterInfoExcelData.WaveId))
            {
                Debug.Log($"ChapterInfo 엑셀 데이터 WaveId : {chapterInfoExcelData.WaveId}값이 겹칩니다.");
            }
			else
			{
				idList.Add(chapterInfoExcelData.WaveId);
			}
		}
    }
}
