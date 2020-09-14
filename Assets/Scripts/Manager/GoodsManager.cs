using GameSparks.Api.Requests;
using GameSparks.Core;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoSingleton<GoodsManager>
{
    private int rewardAmount;
    private int rewardId;
    private PotionGrade randomPotionGrade;
    private List<RuneGrade> randomlyPickedRuneGradeList;
    public List<(int RuneId, bool IsSoldOut)> runeOnSalesList { get; set; }

    public void Initialize()
    {
        LoadRuneOnSalesData();
    }


    public void PurchaseGoods(int goodsId)
    {
        MainManager.instance.backCanvas.uiMainMenu.uiStore.ShowBeingPurchase(); // 구매중 팝업 띄우기.

        new LogEventRequest()
           .SetEventKey("PurchaseGoods")
           .SetEventAttribute("GoodsId", goodsId)
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       rewardAmount = (int)(response.ScriptData.GetInt("RewardAmount"));

                       string strRewardCurrency = (response.ScriptData.GetString("RewardCurrency"));
                       RewardCurrency rewardCurrency = (RewardCurrency)Enum.Parse(typeof(RewardCurrency), strRewardCurrency);

                       switch (rewardCurrency)
                       {
                           case RewardCurrency.Rune:
                               rewardId = (int)(response.ScriptData.GetInt("RewardId"));
                               break;
                       }

                       StartCoroutine(Co_GetItems(rewardCurrency));
                   }
                   else
                   {
                       string strPurchaseCurrency = (response.ScriptData.GetString("PurchaseCurrency"));
                       PurchaseCurrency purchaseCurrency = (PurchaseCurrency)Enum.Parse(typeof(PurchaseCurrency), strPurchaseCurrency);

                       MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                       MainManager.instance.frontCanvas.uiAskGoToStore.SetPurchaseCurrency(purchaseCurrency);
                       UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiAskGoToStore); // 다이아, 골드 구매 창으로 이동할지 물어보는 팝업창 띄우기
                   }
               }
               else
               {
                   MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                   // 서버 문제로 구매 실패 팝업 띄우기.
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void PurchaseRune(int goodsId, int runeOnSalesId, RuneGrade runeGrade)
    {
        MainManager.instance.backCanvas.uiMainMenu.uiStore.ShowBeingPurchase(); // 구매중 팝업 띄우기.

        new LogEventRequest()
           .SetEventKey("PurchaseRune")
           .SetEventAttribute("GoodsId", goodsId)
           .SetEventAttribute("RuneOnSalesId", runeOnSalesId)
           .SetEventAttribute("RuneOnSalesGrade", runeGrade.ToString())
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool isBuyable = (bool)(response.ScriptData.GetBoolean("IsBuyable"));
                   if (isBuyable)
                   {
                       rewardAmount = (int)(response.ScriptData.GetInt("RewardAmount"));

                       string strRewardCurrency = (response.ScriptData.GetString("RewardCurrency"));
                       RewardCurrency rewardCurrency = (RewardCurrency)Enum.Parse(typeof(RewardCurrency), strRewardCurrency);

                       rewardId = (int)(response.ScriptData.GetInt("RewardId"));

                       MainManager.instance.backCanvas.uiMainMenu.uiStore.uiRuneOnSalesList.SetIsSoldOutToId(runeOnSalesId);
                       StartCoroutine(Co_GetItems(rewardCurrency));
                   }
                   else
                   {
                       bool isSoldOut = (bool)(response.ScriptData.GetBoolean("IsSoldOut"));
                       MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                       if (isSoldOut)
                       {
                           Debug.Log("Error is sold out!!!");
                       }
                       else
                       {
                           string strPurchaseCurrency = (response.ScriptData.GetString("PurchaseCurrency"));
                           PurchaseCurrency purchaseCurrency = (PurchaseCurrency)Enum.Parse(typeof(PurchaseCurrency), strPurchaseCurrency);

                           MainManager.instance.frontCanvas.uiAskGoToStore.SetPurchaseCurrency(purchaseCurrency);
                           UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiAskGoToStore); // 다이아, 골드 구매 창으로 이동할지 물어보는 팝업창 띄우기
                       }
                   }
               }
               else
               {
                   MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                   // 서버 문제로 구매 실패 팝업 띄우기.
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void PurchaseRandomRune(int goodsId, RuneRating runeRating)
    {
        MainManager.instance.backCanvas.uiMainMenu.uiStore.ShowBeingPurchase(); // 구매중 팝업 띄우기.

        new LogEventRequest()
           .SetEventKey("PurchaseRandomRune")
           .SetEventAttribute("GoodsId", goodsId)
           .SetEventAttribute("RatingOfRandomRune", runeRating.ToString())
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       rewardAmount = (int)(response.ScriptData.GetInt("RewardAmount"));

                       string strRewardCurrency = (response.ScriptData.GetString("RewardCurrency"));
                       RewardCurrency rewardCurrency = (RewardCurrency)Enum.Parse(typeof(RewardCurrency), strRewardCurrency);

                       var strGradeList = response.ScriptData.GetStringList("RuneGradeList");

                       foreach (var a in strGradeList)
                       {
                           Debug.Log(a);
                       }

                       SetRandomlyPickedRuneGradeList(strGradeList);
                       StartCoroutine(Co_GetItems(rewardCurrency));
                   }
                   else
                   {
                       string strPurchaseCurrency = (response.ScriptData.GetString("PurchaseCurrency"));
                       PurchaseCurrency purchaseCurrency = (PurchaseCurrency)Enum.Parse(typeof(PurchaseCurrency), strPurchaseCurrency);

                       MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                       MainManager.instance.frontCanvas.uiAskGoToStore.SetPurchaseCurrency(purchaseCurrency);
                       UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiAskGoToStore); // 다이아, 골드 구매 창으로 이동할지 물어보는 팝업창 띄우기
                   }
               }
               else
               {
                   MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                   // 서버 문제로 구매 실패 팝업 띄우기.
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void PurchaseRandomPotion(int goodsId)
    {
        MainManager.instance.backCanvas.uiMainMenu.uiStore.ShowBeingPurchase(); // 구매중 팝업 띄우기.

        new LogEventRequest()
           .SetEventKey("PurchaseRandomPotion")
           .SetEventAttribute("GoodsId", goodsId)
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       rewardAmount = (int)(response.ScriptData.GetInt("RewardAmount"));

                       string strRewardCurrency = (response.ScriptData.GetString("RewardCurrency"));
                       RewardCurrency rewardCurrency = (RewardCurrency)Enum.Parse(typeof(RewardCurrency), strRewardCurrency);

                       string strRandomPotionGrade = (response.ScriptData.GetString("RandomGrade"));
                       randomPotionGrade = (PotionGrade)Enum.Parse(typeof(PotionGrade), strRandomPotionGrade);

                       StartCoroutine(Co_GetItems(rewardCurrency));
                   }
                   else
                   {
                       string strPurchaseCurrency = (response.ScriptData.GetString("PurchaseCurrency"));
                       PurchaseCurrency purchaseCurrency = (PurchaseCurrency)Enum.Parse(typeof(PurchaseCurrency), strPurchaseCurrency);

                       MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                       MainManager.instance.frontCanvas.uiAskGoToStore.SetPurchaseCurrency(purchaseCurrency);
                       UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiAskGoToStore); // 다이아, 골드 구매 창으로 이동할지 물어보는 팝업창 띄우기
                   }
               }
               else
               {
                   MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                   // 서버 문제로 구매 실패 팝업 띄우기.
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void PurchaseRandomArtifactPiece(int goodsId, int artifactPieceTotalNumber)
    {
        MainManager.instance.backCanvas.uiMainMenu.uiStore.ShowBeingPurchase(); // 구매중 팝업 띄우기.

        new LogEventRequest()
           .SetEventKey("PurchaseRandomArtifactPiece")
           .SetEventAttribute("GoodsId", goodsId)
           .SetEventAttribute("TotalNumber", artifactPieceTotalNumber)
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       rewardAmount = (int)(response.ScriptData.GetInt("RewardAmount"));

                       string strRewardCurrency = (response.ScriptData.GetString("RewardCurrency"));
                       RewardCurrency rewardCurrency = (RewardCurrency)Enum.Parse(typeof(RewardCurrency), strRewardCurrency);

                       rewardId = (int)(response.ScriptData.GetInt("RewardId"));

                       StartCoroutine(Co_GetItems(rewardCurrency));
                   }
                   else
                   {
                       string strPurchaseCurrency = (response.ScriptData.GetString("PurchaseCurrency"));
                       PurchaseCurrency purchaseCurrency = (PurchaseCurrency)Enum.Parse(typeof(PurchaseCurrency), strPurchaseCurrency);

                       MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                       MainManager.instance.frontCanvas.uiAskGoToStore.SetPurchaseCurrency(purchaseCurrency);
                       UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiAskGoToStore); // 다이아, 골드 구매 창으로 이동할지 물어보는 팝업창 띄우기
                   }
               }
               else
               {
                   MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase();
                   // 서버 문제로 구매 실패 팝업 띄우기.
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    // 구매한 아이템을 플레이어 인벤토리에 넣어주는 함수
    private IEnumerator Co_GetItems(RewardCurrency rewardCurrency)
    {
        PlayerDataManager.Instance.LoadPlayerData();
        yield return new WaitForSeconds(1.0f);

        MainManager.instance.backCanvas.uiMainMenu.uiStore.HideBeginPurchase(); // 구매중 팝업 없애기.
        MainManager.instance.backCanvas.uiMainMenu.uiStore.PlayPurchaseCompletedFloatingText(); // 구매 완료! 띄우기

        switch (rewardCurrency)
        {
            case RewardCurrency.None:
                break;
            case RewardCurrency.Gold:
                break;
            case RewardCurrency.Rune:
                RuneManager.Instance.AddRuneToRuneList(rewardId);
                LoadRuneOnSalesData();
                break;
            case RewardCurrency.RandomRune:
                AddRunesAndShowObtainScreen();
                break;
            case RewardCurrency.RandomPotion:
                SetPotionAndShowObtainedPotion();
                break;
            case RewardCurrency.Coin:
                break;
            case RewardCurrency.Status:
                break;
                break;
            case RewardCurrency.Heart:
                MainManager.instance.backCanvas.uiTopMenu.uiHeart.HeartUpdate();
                break;
            case RewardCurrency.Nothing:
                break;
            default:
                break;
        }
    }

    private void SetRandomlyPickedRuneGradeList(List<string> strRuneGradeList)
    {
        List<RuneGrade> runeGrades = new List<RuneGrade>();

        foreach (var strRuneGrade in strRuneGradeList)
        {
            runeGrades.Add((RuneGrade)Enum.Parse(typeof(RuneGrade), strRuneGrade));
        }

        randomlyPickedRuneGradeList = runeGrades;
    }

    private void AddRunesAndShowObtainScreen()
    {
        List<int> obtainedRandomIds = new List<int>();

        foreach (var runeGrade in randomlyPickedRuneGradeList)
        {
            int randomId = RuneService.GetRandomIdByGrade(runeGrade);
            obtainedRandomIds.Add(randomId);
            RuneManager.Instance.AddRune(randomId);
        }

        // 뽑은 갯수에 따라 획득한 룬 화면 띄우기
        if (randomlyPickedRuneGradeList.Count == GoodsService.MIN_NUMBER_OF_RANDOM_RUNES)
        {
            MainManager.instance.backCanvas.uiMainMenu.uiStore.uiObtainedRuneScreen.SetUIObtainedScreen(obtainedRandomIds[0]);
            UIManager.Instance.ShowNew(MainManager.instance.backCanvas.uiMainMenu.uiStore.uiObtainedRuneScreen);
        }
        else
        {
            MainManager.instance.backCanvas.uiMainMenu.uiStore.uiObtainedRunesScreen.SetUIObtainedRuneList(obtainedRandomIds);
            UIManager.Instance.ShowNew(MainManager.instance.backCanvas.uiMainMenu.uiStore.uiObtainedRunesScreen);
        }
    }

    private void SetPotionAndShowObtainedPotion()
    {
        int randomId = PotionService.GetRandomIdByGrade(randomPotionGrade);
        PotionManager.Instance.SetPotion(randomId);

        MainManager.instance.backCanvas.uiMainMenu.uiStore.uiObtainedPotionScreen.SetUIObtainedScreen(randomId);
        UIManager.Instance.ShowNew(MainManager.instance.backCanvas.uiMainMenu.uiStore.uiObtainedPotionScreen);
    }

    private void InitializeRuneOnSalesData(List<int> runeIdList, bool isResetOnMainMenu = false)
    {
        new LogEventRequest()
           .SetEventKey("InitializeRuneOnSalesData")
           .SetEventAttribute("RuneOnSalesIds", runeIdList)
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   if (isResetOnMainMenu)
                   {
                       LoadRuneOnSalesDataAndInitializeUIRuneOnSalesList();
                   }
                   else
                   {
                       LoadRuneOnSalesData();
                   }
               }
               else
               {
                   Debug.Log("Error InitializeRuneOnSales");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void LoadRuneOnSalesData()
    {
        new LogEventRequest()
           .SetEventKey("LoadRuneOnSalesData")
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   bool result = (bool)(response.ScriptData.GetBoolean("Result"));
                   if (result)
                   {
                       GSData runeOnSalesScriptDataList = response.ScriptData.GetGSData("RuneOnSalesData");
                       JsonData runeOnSalesListJsonObject = JsonDataManager.Instance.LoadJson<JsonData>(runeOnSalesScriptDataList.JSON);

                       runeOnSalesList = new List<(int RuneId, bool IsSoldOut)>();

                       for (int i = 0; i < runeOnSalesListJsonObject.Count; i++)
                       {
                           int id = 0;
                           bool isSoldOut = false;

                           id = int.Parse(runeOnSalesListJsonObject[i]["id"].ToString());
                           isSoldOut = (bool)(runeOnSalesListJsonObject[i]["isSoldOut"]);

                           runeOnSalesList.Add((id, isSoldOut));
                       }
                   }
                   else
                   {
                       ResetRuneOnSales();
                   }
               }
               else
               {
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void LoadRuneOnSalesDataAndInitializeUIRuneOnSalesList()
    {
        new LogEventRequest()
           .SetEventKey("LoadRuneOnSalesData")
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   GSData runeOnSalesScriptDataList = response.ScriptData.GetGSData("RuneOnSalesData");
                   JsonData runeOnSalesListJsonObject = JsonDataManager.Instance.LoadJson<JsonData>(runeOnSalesScriptDataList.JSON);

                   runeOnSalesList = new List<(int RuneId, bool IsSoldOut)>();

                   for (int i = 0; i < runeOnSalesListJsonObject.Count; i++)
                   {
                       int id = 0;
                       bool isSoldOut = false;

                       id = int.Parse(runeOnSalesListJsonObject[i]["id"].ToString());
                       isSoldOut = (bool)(runeOnSalesListJsonObject[i]["isSoldOut"]);

                       runeOnSalesList.Add((id, isSoldOut));
                   }

                   //MainManager.instance.backCanvas.uiMainMenu.uiStore.uiRuneOnSalesList.Initialize();
                   MainManager.instance.backCanvas.uiMainMenu.uiStore.uiRuneOnSalesList.InitializeRuneOnSalesList();
                   Debug.Log("runeslaes 초기화 완료");
               }
               else
               {
                   Debug.Log("Error BuyTest");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }

    public void ResetRuneOnSales(bool isResetOnMainMenu = false)
    {
        new LogEventRequest()
           .SetEventKey("GetRuneOnSalesGradeList")
           .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   List<string> runeOnSalesGradeListStr = response.ScriptData.GetStringList("RuneOnSalesGradeList");
                   List<RuneGrade> runeOnSalesGradeList = RuneService.stringGradeListToRuneGradeList(runeOnSalesGradeListStr);
                   List<int> runeIdList = RuneService.GetRandomIdListByRuneGradeList(runeOnSalesGradeList);

                   if (isResetOnMainMenu)
                   {
                       InitializeRuneOnSalesData(runeIdList, true);
                   }
                   else
                   {
                       InitializeRuneOnSalesData(runeIdList, false);
                   }
               }
               else
               {
                   Debug.Log("Error InitializeRuneOnSales");
                   Debug.Log(response.Errors.JSON);
               }
           });
    }
}
