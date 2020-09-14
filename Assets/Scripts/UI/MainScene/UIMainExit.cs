using UnityEngine;
using UnityEngine.UI;

public class UIMainExit : UIControl
{
    [SerializeField] private Button yesButton = null;

    void Start()
    {
        yesButton.onClick.AddListener(() => {
            GameManager.instance.Quit();
        });
    }
}
