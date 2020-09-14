using UnityEngine;
using UnityEngine.UI;

public class UITierProbability : MonoBehaviour
{
    [SerializeField] private Text oneTierProbability = null;
    [SerializeField] private Text twoTierProbability = null;
    [SerializeField] private Text threeTierProbability = null;
    [SerializeField] private Text fourTierProbability = null;

    public void Initialize()
    {
        UpdateTierProbability();

        InGameManager.instance.playerState.OnLevelUp += UpdateTierProbability;
    }

    public void UpdateTierProbability()
    {
        int currentLevel = InGameManager.instance.playerState.level;

        var probabilityDataSheet = DataBase.Instance.probabilityDataSheet;
        if(probabilityDataSheet.TryGetProbabilityData(currentLevel, out var probabilityData))
        {
            // Gray
            oneTierProbability.text = $"<color=#808080ff>●</color>{probabilityData.OneTier}%";

            //Green
            twoTierProbability.text = $"<color=#008000ff>●</color>{probabilityData.TwoTier}%";

            // DarkBlue
            threeTierProbability.text = $"<color=#0000a0ff>●</color>{probabilityData.ThreeTier}%";

            // Orange
            fourTierProbability.text = $"<color=#ffa500ff>●</color>{probabilityData.FourTier}%";
        }
    }

    private void OnDestroy()
    {
        if(InGameManager.instance != null)
        {
            InGameManager.instance.playerState.OnLevelUp -= UpdateTierProbability;
        }
    }
}
