using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ChapterDataSheet : ScriptableObject, IDataSheet
{
	public List<ChapterExcelData> ChapterExcelDatas;
	private Dictionary<int, ChapterData> ChapterDatas;

	public bool TryGetChapterDatas(out Dictionary<int, ChapterData> chapterDatas)
	{
		chapterDatas = new Dictionary<int, ChapterData>(ChapterDatas);
		if(chapterDatas != null)
		{
			return true;
		}

		Debug.LogError("Error TryGetChapterDatas");
		return false;
	}

	public bool TryGetChapterTotalWave(int chapterId, out int totalWave)
	{
		totalWave = 0;

		if (TryGetChapterData(chapterId, out var chapterData))
		{
			totalWave = chapterData.TotalWave;
			return true;
		}

		Debug.LogError($"Error TryGetChapterTotalWave chapterId:{chapterId}");
		return false;
	}

	public bool TryGetChapterName(int chapterId, out string name)
	{
		name = "";

		if (TryGetChapterData(chapterId, out var chapterData))
		{
			name = chapterData.Name;
			return true;
		}

		Debug.LogError($"Error TryGetChapterName chapterId:{chapterId}");
		return false;
	}

	public bool TryGetChapterId(int chapterId, out int id)
	{
		id = 0;

		if (TryGetChapterData(chapterId, out var chapterData))
		{
			id = chapterData.Id;
			return true;
		}

		Debug.LogError($"Error TryGetChapterId chapterId:{chapterId}");
		return false;
	}

	public bool TryGetChapterImage(int chapterId, out Sprite sprite)
	{
		sprite = null;

		if (TryGetChapterData(chapterId, out var chapterData))
		{
			sprite = chapterData.Image;
			return true;
		}

		Debug.LogError($"Error TryGetChapterImage chapterId:{chapterId}");
		return false;
	}

	public bool TryGetChapterData(int chapterId, out ChapterData Data)
	{
		if(ChapterDatas.TryGetValue(chapterId, out Data))
		{
			return true;
		}

		Debug.LogError($"Error TryGetChapterData chapterId:{chapterId}");
		return false;
	}

	public void Initialize()
	{
		GenerateData();
	}

	private void GenerateData()
	{
		ChapterDatas = new Dictionary<int, ChapterData>();

		foreach (var chapterExcelData in ChapterExcelDatas)
		{
			ChapterData chapterData = new ChapterData(chapterExcelData);
			ChapterDatas.Add(chapterData.Id, chapterData);
		}
	}

	public void DataValidate()
	{
		// 아이디가 고유한 값을 가지는지 확인
        List<int> idList = new List<int>();

        foreach (var chapterExcelData in ChapterExcelDatas)
        {
            if (idList.Contains(chapterExcelData.Id))
            {
                Debug.Log($"Chapter 엑셀 데이터 Id : {chapterExcelData.Id}값이 겹칩니다.");
            }
			else
			{
				idList.Add(chapterExcelData.Id);
			}
        }
    }
}
