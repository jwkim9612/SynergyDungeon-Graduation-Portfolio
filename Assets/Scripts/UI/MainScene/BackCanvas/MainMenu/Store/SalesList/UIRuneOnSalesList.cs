using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRuneOnSalesList : MonoBehaviour
{
    [SerializeField] private Text remainingTimeOfResetText = null;
    [SerializeField] private Button renewalButton = null;
    private Dictionary<int, UIRuneGoods> uiRuneOnSalesList;
    public List<(int RuneId, bool IsSoldOut)> runeOnSalesList { get; set; }
     
    public void Initialize()
    {
        InitializeRuneOnSalesList();
        InitializeRemainingTimeOfReset();

        SetRenewalButton();
    }

    private void SetRenewalButton()
    {
        renewalButton.onClick.AddListener(() =>
        {
            GoodsManager.Instance.ResetRuneOnSales(true);
        });
    }

    public void InitializeRuneOnSalesList()
    {
        uiRuneOnSalesList = new Dictionary<int, UIRuneGoods>();

        runeOnSalesList = GoodsManager.Instance.runeOnSalesList;
        var uiRuneGoods = GetComponentsInChildren<UIRuneGoods>().ToList();


        var runePurchaseableLevelDataSheet = DataBase.Instance.runePurchaseableLevelDataSheet;
        var goodsDataSheet = DataBase.Instance.goodsDataSheet;

        int listIndex = 0;
        
        if(runePurchaseableLevelDataSheet.TryGetRunePurchaseableLevelDatas(out var runePurchaseableLevelDatas))
        {
            for (int id = GoodsService.FIRST_RUNE_SALES_ID; id <= runePurchaseableLevelDatas.Count; ++id)
            {
                uiRuneOnSalesList.Add(id, uiRuneGoods[listIndex]);

                int goodsId = id;
                if (goodsDataSheet.TryGetGoodsData(goodsId, out var goodsData))
                {
                    uiRuneOnSalesList[id].SetUIGoods(goodsData, goodsId, runeOnSalesList[listIndex].RuneId, goodsId, runeOnSalesList[listIndex].IsSoldOut);
                    ++listIndex;
                }
            }
        }
    }

    private void InitializeRemainingTimeOfReset()
    {
        StartCoroutine(Co_PlayRemainingTimeOfReset());
    }

    private IEnumerator Co_PlayRemainingTimeOfReset()
    {
        int hour = (int)TimeManager.Instance.remainingTimeOfAttendance / 60 / 60;
        int minute = (int)TimeManager.Instance.remainingTimeOfAttendance / 60 % 60;
        int second = (int)TimeManager.Instance.remainingTimeOfAttendance % 60;
        remainingTimeOfResetText.text = "남은시간 : " + hour + "시간 " + minute + "분";

        while (hour != 0 || minute != 0 || second != 0)
        {
            yield return new WaitForSeconds(1.0f);
            if (second == 0)
            {
                if (minute == 0)
                {
                    if (hour == 0)
                    {
                        break;
                    }

                    --hour;
                    minute = 60;
                }

                --minute;
                second = 59;
                remainingTimeOfResetText.text = "남은시간 : " + hour + "시간 " + minute + "분";
            }
            else
                --second;
        }

        MainManager.instance.frontCanvas.ShowConnecting();
        yield return new WaitForSeconds(2.0f);
        TimeManager.Instance.AttendanceCheck(true);
        yield return new WaitForSeconds(2.0f);
        MainManager.instance.frontCanvas.HideConnecting();
    }

    public void SetIsSoldOutToId(int id)
    {
        uiRuneOnSalesList[id].SetIsSoldOut();
    }
}
