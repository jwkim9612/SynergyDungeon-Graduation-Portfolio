using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIGoldSalesList : MonoBehaviour
{
    private List<UIAmountRequiredGoods> uiGoldGoodsList;

    public void Initialize()
    {
        SetUIGoldGoodsList();
    }

    public void SetUIGoldGoodsList()
    {
        uiGoldGoodsList = GetComponentsInChildren<UIAmountRequiredGoods>().ToList();

        var goldSalesIdList = GoodsService.GOLD_SALES_ID_LIST;
        var goodsDataSheet = DataBase.Instance.goodsDataSheet;
        for (int i = 0; i < goldSalesIdList.Count; ++i)
        {
            if (goodsDataSheet.TryGetGoodsData(goldSalesIdList[i], out var goodsData))
            {
                uiGoldGoodsList[i].SetUIGoods(goodsData, goldSalesIdList[i]);
            }
        }
    }
}
