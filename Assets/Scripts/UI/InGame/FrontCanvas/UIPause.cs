using UnityEngine;
using UnityEngine.UI;

public class UIPause : UIControl
{
    [SerializeField] private Button continueButton = null;
    [SerializeField] private Button mainMenuButton = null;
    [SerializeField] private UIAskBackToMainMenu uiAskBackToMainMenu = null;

    public void Initialize()
    {
        uiAskBackToMainMenu.Initialize();

        SetContinueButton();
        SetMainMenuButton();
    }

    private void SetContinueButton()
    {
        continueButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();

            var currentSpeed = InGameManager.instance.backCanvas.uiBottomMenu.uiBattleMenu.uiSpeedController.currentSpeed;
            Time.timeScale = currentSpeed;
        });
    }

    private void SetMainMenuButton()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowNew(uiAskBackToMainMenu);
        });
    }
}
