using UnityEngine;
using UnityEngine.UI;

public class AskInGameContinue : UIControl
{
    [SerializeField] private Button yesButton = null;
    [SerializeField] private Button noButton = null;

    public void Initialize()
    {
        SetYesButton();
        SetNoButton();
    }

    private void SetYesButton()
    {
        yesButton.onClick.AddListener(() =>
        {
            MainManager.instance.frontCanvas.ShowEnteringDungeon();
            SaveManager.Instance.LoadInGameDataAndLoadInGameScene();
            OnHide();
        });
    }

    private void SetNoButton()
    {
        noButton.onClick.AddListener(() =>
        {
            SaveManager.Instance.RemoveInGameData();
            OnHide();
        });
    }
}
