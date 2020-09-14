public class UIRandomPotionGoods : UIGoods
{
    public void SetUIGoods(GoodsData goodsData, int goodsId)
    {
        SetGoodsName(goodsData.Name);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);

        showAskPurchaseButton.onClick.AddListener(() =>
        {
            bool hasPotionInUse = PotionManager.Instance.HasPotionInUse();

            var uiAskPurchaseForRandomPotionGoods = MainManager.instance.backCanvas.uiMainMenu.uiStore.uiAskPurchaseForRandomPotionGoods;
            uiAskPurchaseForRandomPotionGoods.SetUIAskPurchase(goodsData, goodsId, hasPotionInUse);
            UIManager.Instance.ShowNew(uiAskPurchaseForRandomPotionGoods);
        });
    }
}
