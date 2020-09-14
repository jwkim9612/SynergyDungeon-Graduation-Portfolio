using UnityEngine;
using UnityEngine.UI;

public class UIDiamond : MonoBehaviour
{
    [SerializeField] private Text diamondText = null;

    public void Initialize()
    {
        PlayerDataManager.Instance.OnDiamondChanged += UpdateDiamondText;
        UpdateDiamondText();
    }

    public void UpdateDiamondText()
    {
        diamondText.text = string.Format("{0}", PlayerDataManager.Instance.playerData.Diamond.ToString("#,##0"));
    }

    private void OnDestroy()
    {
        if(PlayerDataManager.IsAlive)
        {
            PlayerDataManager.Instance.OnDiamondChanged -= UpdateDiamondText;
        }
    }
}
