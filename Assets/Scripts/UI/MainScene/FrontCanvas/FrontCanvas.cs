using UnityEngine;

public class FrontCanvas : MonoBehaviour
{
    public UIAskGoToStore uiAskGoToStore;
    public AskInGameContinue askInGameContinue;
    public UIHeartRefill uiHeartRefill;
    public UIPurchaseGold uiPurchaseGold;
    
    [SerializeField] private GameObject connecting = null;
    [SerializeField] private GameObject EnteringDungeon = null;

    public void Initialize()
    {
        uiAskGoToStore.Initialize();
        askInGameContinue.Initialize();
        uiHeartRefill.Initialize();
        uiPurchaseGold.Initialize();
    }

    public void ShowAskInGameContinue()
    {
        askInGameContinue.gameObject.SetActive(true);
    }

    public void ShowConnecting()
    {
        connecting.SetActive(true);
    }

    public void HideConnecting()
    {
        connecting.SetActive(false);
    }

    public void ShowEnteringDungeon()
    {
        EnteringDungeon.SetActive(true);
    }

    public void HideEnteringDungeon()
    {
        EnteringDungeon.SetActive(false);
    }
}
