using UnityEngine;
using UnityEngine.UI;

public class UIAskPurchase : UIControl
{
    [SerializeField] private Text askPurchaseText = null;
    [SerializeField] private Image goodsImage = null;
    [SerializeField] protected Text goodsPrice = null;
    [SerializeField] private Image purchaseCurrencyImage = null;
    [SerializeField] protected Button purchaseButton = null;
    protected int goodsId;

    public virtual void Initialize()
    {

    }

    protected void SetAskPurchaseText(string name)
    {
        askPurchaseText.text = name + "를 구매하시겠습니까?";
    }

    protected void SetPurchaseCurrencyImage(PurchaseCurrency purchaseCurrency)
    {
        switch (purchaseCurrency)
        {
            case PurchaseCurrency.Gold:
                purchaseCurrencyImage.sprite = GoodsService.GOLD_IMAGE;
                break;
            case PurchaseCurrency.Diamond:
                purchaseCurrencyImage.sprite = GoodsService.DIAMOND_IMAGE;
                break;
            default:
                Debug.LogError("Error SetPurchaseCurrencyImage!!");
                break;
        }
    }

    protected void SetGoodsPrice(int price, PurchaseCurrency purchaseCurrency)
    {
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

    protected void SetGoodsImage(Sprite image)
    {
        goodsImage.sprite = image;
    }
}
