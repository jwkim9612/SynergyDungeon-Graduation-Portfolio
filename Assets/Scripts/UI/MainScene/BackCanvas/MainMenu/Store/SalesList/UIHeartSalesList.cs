using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIHeartSalesList : MonoBehaviour
{
    private List<UIAmountRequiredGoods> uiHeartGoodsList;

    public void Initialize()
    {
        SetUIHeartGoodsList();
    }

    public void SetUIHeartGoodsList()
    {
        uiHeartGoodsList = GetComponentsInChildren<UIAmountRequiredGoods>().ToList();

        var heartSalesIdList = GoodsService.HEART_SALES_ID_LIST;
        var goodsDataSheet = DataBase.Instance.goodsDataSheet;
        for (int i = 0; i < heartSalesIdList.Count; ++i)
        {
            if (goodsDataSheet.TryGetGoodsData(heartSalesIdList[i], out var goodsData))
            {
                uiHeartGoodsList[i].SetUIGoods(goodsData, heartSalesIdList[i]);
            }
        }
    }
}
