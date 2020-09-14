using UnityEngine;
using UnityEngine.UI;

public class UISynergy : MonoBehaviour
{
    public Image synergyImage = null;
    [SerializeField] protected Toggle toggle = null;

    public UIInGameSynergyInfo uiInGameSynergyInfo { get; set; } = null;

    public virtual void Initialize()
    {
        toggle = GetComponent<Toggle>();
    }

    public void SetImage(Sprite sprite)
    {
        synergyImage.sprite = sprite;
    }
}
