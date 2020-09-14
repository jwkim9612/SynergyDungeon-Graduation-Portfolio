using UnityEngine;

public class InGameFrontCanvas : MonoBehaviour
{
    public UIPause uiPause;
    public UIEndStage uiStageClear;
    public UIGameOver uiGameOver;
    public UICanNotStart uiCanNotStart;

    public void Initialize()
    {
        uiPause.Initialize();
        uiStageClear.Initialize();
        uiGameOver.Initialize();
    }

    public void ShowGameOver()
    {
        uiGameOver.OnShow();
    }

    public void ShowStageClear()
    {
        uiStageClear.OnShow();
    }
}
