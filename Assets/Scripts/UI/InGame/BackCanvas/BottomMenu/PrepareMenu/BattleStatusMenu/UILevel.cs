using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    [SerializeField] private Text levelText = null;

    public void Initialize()
    {
        UpdateLevelText();

        InGameManager.instance.playerState.OnLevelUp += UpdateLevelText;
    }

    public void UpdateLevelText()
    {
        int level = InGameManager.instance.playerState.level;

        levelText.text = level.ToString();
    }
}
