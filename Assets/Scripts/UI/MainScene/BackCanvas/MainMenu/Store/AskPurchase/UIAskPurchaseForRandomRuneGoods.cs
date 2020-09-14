using UnityEngine;
using UnityEngine.UI;

public class UIAskPurchaseForRandomRuneGoods : UIAskPurchase
{
    [SerializeField] private Text goodsAmount = null;
    private RuneRating runeRating;

    public override void Initialize()
    {
        SetPurchaseButton();
    }

    private void SetPurchaseButton()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();
            Debug.Log(runeRating);
            GoodsManager.Instance.PurchaseRandomRune(goodsId, runeRating);
        });
    }

    public void SetUIAskPurchase(GoodsData goodsData, int goodsId, RuneRating runeRating)
    {
        SetAskPurchaseText(goodsData.Name);
        SetGoodsAmount(goodsData.RewardAmount);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice, goodsData.PurchaseCurrency);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);

        this.goodsId = goodsId;
        this.runeRating = runeRating;
    }

    private void SetGoodsAmount(int amount)
    {
        goodsAmount.text = amount.ToString();
    }
}
