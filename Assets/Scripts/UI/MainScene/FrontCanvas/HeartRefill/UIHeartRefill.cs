using UnityEngine;

public class UIHeartRefill : UIControl
{
    public UIHeartTimer uiHeartTimer;
    public UIHeartSalesList uiHeartSalesList;

    public void Initialize()
    {
        uiHeartTimer.Initialize();
        uiHeartSalesList.Initialize();
    }
}
