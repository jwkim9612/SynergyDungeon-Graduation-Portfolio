using UnityEngine;
using UnityEngine.UI;

public class UIInGameSynergyInfoCharacter : MonoBehaviour
{
    [SerializeField] private Image characterImage = null;
    [SerializeField] private Image characterBackground = null;

    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    public void Activate()
    {
        characterBackground.color = Color.white;
    }

    public void Disabled()
    {
        characterBackground.color = new Color(1, 1, 1, 0f);
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }

    public void OnShow()
    {
        this.gameObject.SetActive(true);
    }
}
