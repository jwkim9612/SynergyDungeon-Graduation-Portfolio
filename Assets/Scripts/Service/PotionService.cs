using Shared.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionService
{
    public const string DEFAULT_IMAGE_PATH = "Images/Main/Battlefield/Empty_Potion";
    public static Sprite DEFAULT_IMAGE = Resources.Load<Sprite>(DEFAULT_IMAGE_PATH);

    public const string DEFAULT_POTION_DESCRIPTION = "다음 게임에 적용되는 포션이 있는 공간입니다.";


    public static Dictionary<PotionGrade, List<int>> ListOfPotionIdsByGrade;

    public static void Initialize()
    {
        InitializeListOfPotionIdsByGrade();
    }

    private static void InitializeListOfPotionIdsByGrade()
    {
        // 각 등급별로 초기화
        ListOfPotionIdsByGrade = new Dictionary<PotionGrade, List<int>>();
        foreach (PotionGrade potionGrade in Enum.GetValues(typeof(PotionGrade)))
        {
            if (potionGrade == PotionGrade.None)
                continue;

            ListOfPotionIdsByGrade.Add(potionGrade, new List<int>());
        }

        // 포션 데이터를 돌면서 각 등급에 맞게 id값 넣어주기.
        var potionDataSheet = DataBase.Instance.potionDataSheet;
        if (potionDataSheet == null)
        {
            Debug.LogError("Error potionDataSheet is null");
            return;
        }

        if (potionDataSheet.TryGetPotionDatas(out var potionDatas))
        {
            foreach (var potionData in potionDatas)
            {
                ListOfPotionIdsByGrade[potionData.Value.Grade].Add(potionData.Key);
            }
        }
    }

    public static int GetRandomIdByGrade(PotionGrade potionGrade)
    {
        var potionIdList = ListOfPotionIdsByGrade[potionGrade];
        int randomIndex = RandomService.RandRange(0, potionIdList.Count - 1);

        return potionIdList[randomIndex];
    }

    public static string GetNameStrByGrade(PotionGrade potionGrade)
    {
        string gradeStr = "";

        switch (potionGrade)
        {
            case PotionGrade.None:
                gradeStr = "에러";
                break;
            case PotionGrade.F:
                gradeStr = "F등급";
                break;
            case PotionGrade.D:
                gradeStr = "D등급";
                break;
            case PotionGrade.C:
                gradeStr = "C등급";
                break;
            case PotionGrade.B:
                gradeStr = "B등급";
                break;
            case PotionGrade.A:
                gradeStr = "A등급";
                break;
            default:
                break;
        }
        return gradeStr;
    }
}
