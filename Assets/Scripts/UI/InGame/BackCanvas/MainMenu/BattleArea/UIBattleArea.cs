using UnityEngine;

public class UIBattleArea : MonoBehaviour
{
    [SerializeField] private UICharacterArea uiCharacterArea = null;
    [SerializeField] private UIPlacementStatus uiPlacementStatus = null;
    public UIEnemyArea uiEnemyArea = null;
    public BattleStatus battleStatus = null;

    private bool isFirstTime;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += BattleStart;
        InGameManager.instance.gameState.OnBattle += SpaceExpansion;
        InGameManager.instance.gameState.OnPrepare += SpaceReduction;

        // uiCharacterArea는 DraggableCentral에서 초기화해줌.
        uiEnemyArea.Initialize();
        uiPlacementStatus.Initialize();

        isFirstTime = true;
    }

    private void SpaceExpansion()
    {
        RectTransform rec = transform as RectTransform;
        rec.Translate(new Vector3(0.0f, -InGameService.DISTANCE_TO_MOVE_AT_START_OF_BATTLE, 0.0f));
    }

    private void SpaceReduction()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            return;
        }

        RectTransform rec = transform as RectTransform;
        rec.Translate(new Vector3(0.0f, InGameService.DISTANCE_TO_MOVE_AT_START_OF_BATTLE, 0.0f));
    }

    private void BattleStart()
    {
        battleStatus.characters = uiCharacterArea.GetCharacterList();
        battleStatus.enemies = uiEnemyArea.GetEnemyList();
        battleStatus.BattleStart();
    }
}
