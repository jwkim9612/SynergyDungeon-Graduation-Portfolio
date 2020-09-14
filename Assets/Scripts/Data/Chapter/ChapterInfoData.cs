using System.Collections.Generic;

public class ChapterInfoData
{
    public ChapterInfoData(ChapterInfoExcelData chapterInfoExcelData)
    {
        ChapterId = chapterInfoExcelData.ChapterId;
        WaveId = chapterInfoExcelData.WaveId;
        StageId = chapterInfoExcelData.StageId;
        GoldAmount = chapterInfoExcelData.GoldAmount;
        ExpAmount = chapterInfoExcelData.ExpAmount;
        EnemyIds = chapterInfoExcelData.EnemyIds;
        FrontIds = chapterInfoExcelData.FrontIds;
        BackIds = chapterInfoExcelData.BackIds;
        ClearGoldReward = chapterInfoExcelData.ClearGoldReward;
        ClearExpReward = chapterInfoExcelData.ClearExpReward;

        InitializeEnemyIds();
        InitializeFrontIds();
        InitializeBackIds();
    }

    public int ChapterId;
    public int WaveId;
    public int StageId;
    public int GoldAmount;
    public int ExpAmount;
    public string EnemyIds;
    public string FrontIds;
    public string BackIds;
    public int ClearGoldReward;
    public int ClearExpReward;

    public List<int> EnemyIdList;
    public List<int> FrontIdList;
    public List<int> BackIdList;

    // 적들의 아이디를 초기화
    private void InitializeEnemyIds()
    {
        EnemyIdList = new List<int>();

        string[] enemyIdsStr = EnemyIds.Split(',');
        foreach (var enemyId in enemyIdsStr)
        {
            EnemyIdList.Add(enemyId[0] - '0');
        }
    }

    // 적을 앞라인 몇 번째의 위치에 생성할 것인지를 초기화
    private void InitializeFrontIds()
    {
        if (FrontIds == "")
            return;

        FrontIdList = new List<int>();

        string[] frontIdsStr = FrontIds.Split(',');
        foreach (var frontId in frontIdsStr)
        {
            FrontIdList.Add(frontId[0] - '0');
        }
    }

    // 적을 뒷라인 몇 번째의 위치에 생성할 것인지를 초기화
    private void InitializeBackIds()
    {
        if (BackIds == "")
            return;

        BackIdList = new List<int>();

        string[] backIdsStr = BackIds.Split(',');
        foreach (var backId in backIdsStr)
        {
            BackIdList.Add(backId[0] - '0');
        }
    }
}
