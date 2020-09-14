using UnityEngine;
using UnityEngine.UI;

public class UIAskExit : UIControl
{
    [SerializeField] private Button yesButton = null;
    [SerializeField] private Button noButton = null;

    void Start()
    {
        yesButton.onClick.AddListener(() =>
        {
            GameManager.instance.Quit();
        });

        noButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();
        });
    }
}
