using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIRandomRuneSalesList : MonoBehaviour
{
    private List<UIRandomRuneGoods> uiRandomRuneGoodsList;

    public void Initialize()
    {
        SetUIRandomRuneGoodsLIst();
    }

    private void SetUIRandomRuneGoodsLIst()
    {
        uiRandomRuneGoodsList = GetComponentsInChildren<UIRandomRuneGoods>().ToList();

        var randomRuneIdAndRatingList = GoodsService.RANDOM_RUNE_SALES_ID_AND_RATING_LIST;
        var goodsDataSheet = DataBase.Instance.goodsDataSheet;
        if (goodsDataSheet == null)
        {
            Debug.LogError("Error goodsDataSheet is null");
            return;
        }

        for (int i = 0; i < randomRuneIdAndRatingList.Count; ++i)
        {
            if (goodsDataSheet.TryGetGoodsData(randomRuneIdAndRatingList[i].runeId, out var goodsData))
            {
                uiRandomRuneGoodsList[i].SetUIGoods(goodsData, randomRuneIdAndRatingList[i].runeId, randomRuneIdAndRatingList[i].rating);
            }
        }
    }
}
