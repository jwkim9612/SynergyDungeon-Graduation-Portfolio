using UnityEngine;
using UnityEngine.UI;

public class UIExpBar : MonoBehaviour
{
    [SerializeField] private Text expText = null;
    [SerializeField] private Image expImage = null;

    public void Initialize()
    {
        UpdateExp();

        InGameManager.instance.playerState.OnExpChanged += UpdateExp;
    }

    public void UpdateExp()
    {
        var playerState = InGameManager.instance.playerState;

        if (playerState.IsMaxLevel())
        {
            expText.text = "Max";
            expImage.fillAmount = 1.0f;
        }
        else
        {
            int exp = playerState.exp;
            int satisfyExp = playerState.SatisfyExp;

            expText.text = exp + "/" + satisfyExp;
            expImage.fillAmount = (float)exp / satisfyExp;
        }
    }
}
