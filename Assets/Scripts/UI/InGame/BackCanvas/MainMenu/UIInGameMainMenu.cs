using UnityEngine;

public class UIInGameMainMenu : MonoBehaviour
{
    public UIBattleArea uiBattleArea;
    public UIAbilityEffectList uiAbilityEffectList;

    public void Initialize()
    {
        uiBattleArea.Initialize();
        uiAbilityEffectList.Initialize();
    }
}
