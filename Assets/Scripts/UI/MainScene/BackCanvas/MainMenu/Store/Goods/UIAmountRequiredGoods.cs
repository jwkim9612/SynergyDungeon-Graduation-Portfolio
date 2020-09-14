using UnityEngine;
using UnityEngine.UI;

public class UIAmountRequiredGoods : UIGoods
{
    [SerializeField] private Text goodsAmount = null;

    public void SetUIGoods(GoodsData goodsData, int goodsId)
    {
        SetGoodsName(goodsData.Name);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);
        SetGoodsAmount(goodsData.RewardAmount);

        showAskPurchaseButton.onClick.AddListener(() =>
        {
            var uiAskPurchaseForAmountRequiredGoods = MainManager.instance.backCanvas.uiMainMenu.uiStore.uiAskPurchaseForAmountRequiredGoods;
            uiAskPurchaseForAmountRequiredGoods.SetUIAskPurchase(goodsData, goodsId);
            UIManager.Instance.ShowNew(uiAskPurchaseForAmountRequiredGoods);
        });
    }

    private void SetGoodsAmount(int amount)
    {
        goodsAmount.text = amount.ToString();
    }
}
