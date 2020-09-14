using Shared.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RuneService
{
    public static Dictionary<RuneGrade, List<int>> ListOfRuneIdsByGrade;

    public static void Initialize()
    {
        InitializeListOfRuneIdsByGrade();
    }

    private static void InitializeListOfRuneIdsByGrade()
    {
        // 각 등급별로 초기화
        ListOfRuneIdsByGrade = new Dictionary<RuneGrade, List<int>>();
        foreach (RuneGrade runeGrade in Enum.GetValues(typeof(RuneGrade)))
        {
            if (runeGrade == RuneGrade.None)
                continue;

            ListOfRuneIdsByGrade.Add(runeGrade, new List<int>());
        }

        // 룬 데이터를 돌면서 각 등급에 맞게 id값 넣어주기.
        var runeDataSheet = DataBase.Instance.runeDataSheet;
        if(runeDataSheet == null)
        {
            Debug.LogError("Error runeDataSheet is null");
            return;
        }

        if(runeDataSheet.TryGetRuneDatas(out var runeDatas))
        {
            foreach (var runeData in runeDatas)
            {
                ListOfRuneIdsByGrade[runeData.Value.Grade].Add(runeData.Key);
            }
        }
    }

    public static int GetRandomIdByGrade(RuneGrade runeGrade)
    {
        var runeIdList = ListOfRuneIdsByGrade[runeGrade];
        int randomIndex = RandomService.RandRange(0, runeIdList.Count - 1);

        return runeIdList[randomIndex];      
    }

    public static List<RuneGrade> stringGradeListToRuneGradeList(List<string> runeGradeListStr)
    {
        List<RuneGrade> runeGradeList = new List<RuneGrade>();
        for (int i = 0; i < runeGradeListStr.Count; ++i)
        {
            runeGradeList.Add((RuneGrade)Enum.Parse(typeof(RuneGrade), runeGradeListStr[i]));
        }

        return runeGradeList;
    }

    public static List<int> GetRandomIdListByRuneGradeList(List<RuneGrade> runeGradeList)
    {
        List<int> idList = new List<int>();
        for(int i = 0; i < runeGradeList.Count; ++i)
        {
            idList.Add(GetRandomIdByGrade(runeGradeList[i]));
        }

        return idList;
    }

    public static string GetNameStrByGrade(RuneGrade runeGrade)
    {
        string gradeStr = "";

        switch (runeGrade)
        {
            case RuneGrade.None:
                gradeStr = "에러";
                break;
            case RuneGrade.F_0:
                gradeStr = "F등급";
                break;
            case RuneGrade.D_0:
                gradeStr = "D등급";
                break;
            case RuneGrade.C_0:
                gradeStr = "C등급";
                break;
            case RuneGrade.B_0:
                gradeStr = "B등급";
                break;
            case RuneGrade.A_0:
                gradeStr = "A등급";
                break;
            case RuneGrade.S_0:
                gradeStr = "S등급";
                break;
            case RuneGrade.S_1:
                gradeStr = "S+등급";
                break;
            case RuneGrade.SS_0:
                gradeStr = "SS등급";
                break;
            case RuneGrade.SS_1:
                gradeStr = "SS+등급";
                break;
            case RuneGrade.SS_2:
                gradeStr = "SS++등급";
                break;
            case RuneGrade.SSS_0:
                gradeStr = "SSS등급";
                break;
            case RuneGrade.SSS_1:
                gradeStr = "SSS+등급";
                break;
            case RuneGrade.SSS_2:
                gradeStr = "SSS++등급";
                break;
            case RuneGrade.SSS_3:
                gradeStr = "SSS+++등급";
                break;
            default:
                break;
        }

        return gradeStr;
    }

    public static bool IsPlusGrade(RuneGrade runeGrade)
    {
        if (runeGrade == RuneGrade.S_1  || runeGrade == RuneGrade.SS_1 || runeGrade == RuneGrade.SS_2 
            || runeGrade == RuneGrade.SSS_1 || runeGrade == RuneGrade.SSS_2 || runeGrade == RuneGrade.SSS_3)
            return true;
        else
            return false;
    }

    public static int GetPriceOfPlusGrade(RuneGrade runeGrade)
    {
        int price = 0;

        switch (runeGrade)
        {
            case RuneGrade.S_1:
                price = 100;
                break;
            case RuneGrade.SS_1:
                price = 200;
                break;
            case RuneGrade.SS_2:
                price = 300;
                break;
            case RuneGrade.SSS_1:
                price = 400;
                break;
            case RuneGrade.SSS_2:
                price = 500;
                break;
            case RuneGrade.SSS_3:
                price = 600;
                break;
            default:
                price = -1;
                break;
        }

        return price;
    }
    
    public const int NUMBER_OF_RUNE_SOCKETS = 5;

    public const int INDEX_OF_ARCHER_SOCKET = 0;
    public const int INDEX_OF_PALADIN_SOCKET = 1;
    public const int INDEX_OF_THIEF_SOCKET = 2;
    public const int INDEX_OF_WARRIOR_SOCKET = 3;
    public const int INDEX_OF_WIZARD_SOCKET = 4;

    public const int TOTAL_NUMBER_PER_LINE = 7;

    public const SortBy DEFAULT_SORT_BY = SortBy.Grade;
    public const string TEXT_OF_SORT_BY_GRADE = "등급 순으로 정렬";
    public const string TEXT_OF_SORT_BY_SOCKET = "슬롯 순으로 정렬";

    public const int NUMBER_OF_CAN_COMBINATION = 5;
}
