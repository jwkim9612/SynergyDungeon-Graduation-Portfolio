using UnityEngine;
using UnityEngine.UI;

public class UIRandomRuneGoods : UIGoods
{
    [SerializeField] private Text goodsAmount = null;

    public void SetUIGoods(GoodsData goodsData, int goodsId, RuneRating runeRating)
    {
        SetGoodsName(goodsData.Name);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);
        SetGoodsAmount(goodsData.RewardAmount);

        showAskPurchaseButton.onClick.AddListener(() =>
        {
            var uiAskPurchaseForRandomRuneGoods = MainManager.instance.backCanvas.uiMainMenu.uiStore.uiAskPurchaseForRandomRuneGoods;
            uiAskPurchaseForRandomRuneGoods.SetUIAskPurchase(goodsData, goodsId, runeRating);
            UIManager.Instance.ShowNew(uiAskPurchaseForRandomRuneGoods);
        });
    }

    private void SetGoodsAmount(int amount)
    {
        goodsAmount.text = amount.ToString();
    }
}
