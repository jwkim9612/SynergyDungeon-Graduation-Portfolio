using UnityEngine;
using UnityEngine.UI;

public class UIRuneGoods : UIGoods
{
    [SerializeField] private Text goodsGrade = null;
    [SerializeField] private UILock uiLock = null;
    [SerializeField] private GameObject SoldOut = null;
    private int runeOnSalesId;
    private bool isLocked = false;

    public void SetUIGoods(GoodsData goodsData, int goodsId, int runeId, int salesId, bool isSoldOut)
    {
        var runeDataSheet = DataBase.Instance.runeDataSheet;
        if(runeDataSheet.TryGetRuneData(runeId, out var runeData))
        {
            runeOnSalesId = salesId;
            SetLocked();

            if (!isLocked)
            {
                SetGoodsName(runeData.Name);
                SetGoodsImage(runeData.Image);
                SetGoodsGrade(runeData.Grade);
                SetGoodsPrice(goodsData.PurchasePrice, runeData.Grade);
                SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);

                if (isSoldOut)
                {
                    SetIsSoldOut();
                }
                else
                {
                    SoldOut.SetActive(false);
                    showAskPurchaseButton.onClick.RemoveAllListeners();
                    showAskPurchaseButton.onClick.AddListener(() =>
                    {
                        var uiAskPurchaseForRuneGoods = MainManager.instance.backCanvas.uiMainMenu.uiStore.uiAskPurchaseForRuneGoods;
                        uiAskPurchaseForRuneGoods.SetUIAskPurchase(goodsData, goodsId, runeData, runeOnSalesId);
                        UIManager.Instance.ShowNew(uiAskPurchaseForRuneGoods);
                    });
                }
            }
        }

    }

    public void SetIsSoldOut()
    {
        SoldOut.SetActive(true);
        showAskPurchaseButton.onClick.RemoveAllListeners();
        showAskPurchaseButton.onClick.AddListener(() =>
        {
            MainManager.instance.backCanvas.uiMainMenu.uiStore.PlaySoldOutFloatingText();
        });
    }

    protected void SetGoodsPrice(int price, RuneGrade runeGrade)
    {
        if(RuneService.IsPlusGrade(runeGrade))
        {
            price = RuneService.GetPriceOfPlusGrade(runeGrade);
        }

        goodsPrice.text = price.ToString();
    }

    public void SetGoodsGrade(RuneGrade runeGrade)
    {
        goodsGrade.text = RuneService.GetNameStrByGrade(runeGrade);
    }

    private void SetLocked()
    {
        var runePurchaseableLevelDataSheet = DataBase.Instance.runePurchaseableLevelDataSheet;
        if(runePurchaseableLevelDataSheet.TryGetRunePurchaseableLevel(runeOnSalesId, out var purchaseableLevel))
        {
            int playerLevel = PlayerDataManager.Instance.playerData.Level;

            if (purchaseableLevel <= playerLevel)
            {
                isLocked = false;
                uiLock.OnHide();
                ActivateAskPurchaseButton();
            }
            else
            {
                isLocked = true;
                uiLock.SetLock(purchaseableLevel);
                uiLock.OnShow();
                DisableAskPurchaseButton();
            }
        }
    }
}
