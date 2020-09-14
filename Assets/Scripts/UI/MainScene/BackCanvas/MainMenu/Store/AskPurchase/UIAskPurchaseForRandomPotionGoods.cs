using UnityEngine;
using UnityEngine.UI;

public class UIAskPurchaseForRandomPotionGoods : UIAskPurchase
{
    [SerializeField] private Text hasPotionInUseText = null;

    public override void Initialize()
    {
        SetPurchaseButton();
    }

    private void SetPurchaseButton()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();
            GoodsManager.Instance.PurchaseRandomPotion(goodsId);
        });
    }

    public void SetUIAskPurchase(GoodsData goodsData, int goodsId, bool hasPotionInUse)
    {
        SetAskPurchaseText(goodsData.Name);
        SetHasPotionInUseText(hasPotionInUse);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice, goodsData.PurchaseCurrency);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);

        this.goodsId = goodsId;
    }

    private void SetHasPotionInUseText(bool hasPotionInUse)
    {
        if(hasPotionInUse)
        {
            hasPotionInUseText.text = "현재 사용중인 물약이 있습니다.";
            return;
        }

        hasPotionInUseText.text = "";
    }
}
