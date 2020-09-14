using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIAskBackToMainMenu : UIControl
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
            SaveManager.Instance.RemoveInGameData();

            Time.timeScale = 1;
            SceneManager.LoadScene("MainScene");
        });
    }

    private void SetNoButton()
    {
        noButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();
        });
    }
}
