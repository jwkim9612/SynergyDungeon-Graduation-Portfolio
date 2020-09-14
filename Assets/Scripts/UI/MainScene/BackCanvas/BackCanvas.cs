using UnityEngine;

public class BackCanvas : MonoBehaviour
{
    public UITopMenu uiTopMenu;
    public UIMainMenu uiMainMenu;

    public void Initialize()
    {
        uiTopMenu.Initialize();
        uiMainMenu.Initialize();
    }
}
