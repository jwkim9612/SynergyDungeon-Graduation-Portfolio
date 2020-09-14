using UnityEngine;

public class UIPurchaseGold : UIControl
{
    [SerializeField] private UIGoldSalesList uiGoldSalesList = null;

    public void Initialize()
    {
        uiGoldSalesList.Initialize();
    }

}
