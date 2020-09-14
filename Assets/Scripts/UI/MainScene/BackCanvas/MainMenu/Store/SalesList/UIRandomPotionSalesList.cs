using UnityEngine;

public class UIRandomPotionSalesList : MonoBehaviour
{
    private UIRandomPotionGoods uiRandomPotionGoods;

    public void Initialize()
    {
        SetUIRandomPotionGoods();
    }

    private void SetUIRandomPotionGoods()
    {
        uiRandomPotionGoods = GetComponentInChildren<UIRandomPotionGoods>();

        var randomPotionSalesId = GoodsService.RANDOM_POTION_SALES_ID;

        var goodsDataSheet = DataBase.Instance.goodsDataSheet;
        if (goodsDataSheet.TryGetGoodsData(randomPotionSalesId, out var goodsData))
        {
            uiRandomPotionGoods.SetUIGoods(goodsData, randomPotionSalesId);
        }
    }
}
