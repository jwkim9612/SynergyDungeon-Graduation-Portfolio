using UnityEngine;

public class UIBattleStatusMenu : MonoBehaviour
{
    [SerializeField] private UILevelAndExp uiLevelAndExp = null;
    [SerializeField] private UICoin uiCoin = null;
    [SerializeField] private UITierProbability uiTierProbability = null;
    [SerializeField] private UIReload uiReload = null;

    public void Initialize()
    {
        uiLevelAndExp.Initialize();
        uiCoin.Initialize();
        uiTierProbability.Initialize();
        uiReload.Initialize();
    }
}
