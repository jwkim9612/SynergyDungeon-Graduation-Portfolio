using UnityEngine;
using UnityEngine.UI;

public class UIStart : UIControl
{
    [SerializeField] private Button startButton = null;

    public void Initialize()
    {
        SetStartButton();

        InGameManager.instance.gameState.OnBattle += OnHide;
        InGameManager.instance.gameState.OnPrepare += OnShow;
    }

    private void SetStartButton()
    {
        startButton.onClick.AddListener(() => {

            var uiCharacterArea = InGameManager.instance.draggableCentral.uiCharacterArea;
            var uiCanNotStart = InGameManager.instance.frontCanvas.uiCanNotStart;

            if (uiCharacterArea.IsEmpty())
            {
                uiCanNotStart.PlayShowCanNotStart();
            }
            else
            {
                uiCanNotStart.HideCanNotStart();

                var gameState = InGameManager.instance.gameState;
                gameState.SetInGameState(InGameState.Battle);
            }
        });
    }
}
