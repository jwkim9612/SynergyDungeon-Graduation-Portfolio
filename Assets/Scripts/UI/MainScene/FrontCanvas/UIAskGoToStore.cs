using UnityEngine;
using UnityEngine.UI;

public class UIAskGoToStore : UIControl
{
    [SerializeField] private Text titleText = null;
    [SerializeField] private Text contentText = null;
    [SerializeField] private Button goToStoreButton = null;

    private PurchaseCurrency purchaseCurrency;

    public void Initialize()
    {
        SetGoToStoreButton();
    }

    private void SetGoToStoreButton()
    {
        goToStoreButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();

            switch (purchaseCurrency)
            {
                case PurchaseCurrency.Gold:
                    UIManager.Instance.ShowNew(MainManager.instance.frontCanvas.uiPurchaseGold);
                    break;
                case PurchaseCurrency.Diamond:
                    
                    break;
                default:
                    Debug.LogError("Error SetGoToStoreButton");
                    break;
            }
        });
    }

    public void SetText()
    {
        switch (purchaseCurrency)
        {
            case PurchaseCurrency.Gold:
                titleText.text = "골드 부족!";
                contentText.text = "골드가 부족합니다. 상점에서 더 구매하세요!";
                break;
            case PurchaseCurrency.Diamond:
                titleText.text = "보석 부족!";
                contentText.text = "보석이 부족합니다. 상점에서 더 구매하세요!";
                break;
            default:
                Debug.LogError("Error UIAskGoToStore SetText");
                break;
        }
    }

    public void SetPurchaseCurrency(PurchaseCurrency purchaseCurrency)
    {
        this.purchaseCurrency = purchaseCurrency;

        SetText();
    }
}
