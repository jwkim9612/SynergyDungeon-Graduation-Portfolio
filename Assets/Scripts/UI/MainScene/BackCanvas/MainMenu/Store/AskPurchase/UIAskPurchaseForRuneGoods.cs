using UnityEngine;

public class UIAskPurchaseForRuneGoods : UIAskPurchase
{
    private int runeOnSalesId;
    private RuneGrade runeGrade;

    public override void Initialize()
    {
        SetPurchaseButton();
    }

    private void SetPurchaseButton()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();
            GoodsManager.Instance.PurchaseRune(goodsId, runeOnSalesId, runeGrade);
        });
    }

    public void SetUIAskPurchase(GoodsData goodsData, int goodsId, RuneData runeData, int salesId)
    {
        SetAskPurchaseText(runeData.Name);
        SetGoodsImage(runeData.Image);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);
        SetRuneGrade(runeData.Grade);
        SetGoodsPrice(goodsData.PurchasePrice, goodsData.PurchaseCurrency);
        runeOnSalesId = salesId;

        this.goodsId = goodsId;
    }

    protected new void SetGoodsPrice(int price, PurchaseCurrency purchaseCurrency)
    {
        if (RuneService.IsPlusGrade(runeGrade))
        {
            price = RuneService.GetPriceOfPlusGrade(runeGrade);
        }

        goodsPrice.text = price.ToString();

        switch (purchaseCurrency)
        {
            case PurchaseCurrency.Gold:
                {
                    if (price <= PlayerDataManager.Instance.playerData.Gold)
                        goodsPrice.color = Color.black;
                    else
                        goodsPrice.color = Color.red;
                }
                break;

            case PurchaseCurrency.Diamond:
                {
                    if (price <= PlayerDataManager.Instance.playerData.Diamond)
                        goodsPrice.color = Color.black;
                    else
                        goodsPrice.color = Color.red;
                }
                break;
            default:
                Debug.LogError("Error SetGoodsPrice!!");
                break;
        }
    }

    private void SetRuneGrade(RuneGrade runeGrade)
    {
        this.runeGrade = runeGrade;
    }
}
