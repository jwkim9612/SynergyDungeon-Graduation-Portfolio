using UnityEngine;

public class UITopMenu : MonoBehaviour
{
    public UIHeart uiHeart;
    public UIGold uiGold;
    public UIDiamond uiDiamond;
    
    public void Initialize()
    {
        uiHeart.Initialize();
        uiGold.Initialize();
        uiDiamond.Initialize();
    }
}
