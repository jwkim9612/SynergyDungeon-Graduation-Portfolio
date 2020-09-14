using UnityEngine;
using UnityEngine.UI;

public class UIEnterDisplayName : UIControl
{
    [SerializeField] private InputField displayNameInputField = null;
    [SerializeField] private Button okButton = null;

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            string displayName;

            if (displayNameInputField.text == "")
                displayName = "Guest";
            else
                displayName = displayNameInputField.text;

            UIManager.Instance.HideAndShowPreview();
            GuestManager.Instance.ChangeDisplayNameAndLoadMainScene(displayName, true);
        });
    }
}
