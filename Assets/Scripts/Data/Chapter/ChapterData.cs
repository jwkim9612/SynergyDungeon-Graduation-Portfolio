using System.Collections.Generic;
using UnityEngine;

public class ChapterData
{
    public ChapterData(ChapterExcelData chapterExcelData)
    {
        Id = chapterExcelData.Id;
        Name = chapterExcelData.Name;
        TotalWave = chapterExcelData.TotalWave;

        Image = Resources.Load<Sprite>(chapterExcelData.ImagePath);

        DataBase.Instance.chapterInfoDataSheet.TryGetChapterInfoDatas(Id, out chapterInfoDatas);
    }

    public int Id;
    public string Name;
    public int TotalWave;
    public Dictionary<int, ChapterInfoData> chapterInfoDatas;
    public Sprite Image;
}
