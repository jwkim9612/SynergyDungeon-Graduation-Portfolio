using UnityEngine;

public class InGameBackCanvas : MonoBehaviour
{
    public UIInGameMainMenu uiMainMenu;
    public UIBottomMenu uiBottomMenu;
    public RectTransform fieldBackground;

    public void Initialize()
    {
        uiMainMenu.Initialize();
        uiBottomMenu.Initialize();

        InGameManager.instance.gameState.OnPrepare += ReduceFieldBackground;
        InGameManager.instance.gameState.OnBattle += ExpansionFieldBackground;
    }

    private void ExpansionFieldBackground()
    {
        fieldBackground.anchorMin = new Vector2(fieldBackground.anchorMin.x, InGameService.FIELD_BACKGROUND_Y_MIN_ANCHOR_IN_BATTLE);
    }

    private void ReduceFieldBackground()
    {
        fieldBackground.anchorMin = new Vector2(fieldBackground.anchorMin.x, InGameService.FIELD_BACKGROUND_Y_MIN_ANCHOR_IN_PREPARE);
    }
}
