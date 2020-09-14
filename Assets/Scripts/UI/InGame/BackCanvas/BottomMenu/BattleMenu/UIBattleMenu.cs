using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIBattleMenu : MonoBehaviour
{
    [SerializeField] private UICharacterStatusList uiFirstCharacterStatusList = null;
    [SerializeField] private UICharacterStatusList uiSecondCharacterStatusList = null;
    public UISpeedController uiSpeedController;

    public List<UICharacterStatus> characterStatusList = null;

    public void Initialize()
    {
        uiFirstCharacterStatusList.Initialize();
        uiSecondCharacterStatusList.Initialize();
        uiSpeedController.Initialize();
        SetCharacterStatusList();

        InGameManager.instance.gameState.OnBattle += InitializeCharacterStatusList;
    }

    private void SetCharacterStatusList()
    {
        characterStatusList = new List<UICharacterStatus>();
        characterStatusList.AddRange(uiFirstCharacterStatusList.characterStatusList);
        characterStatusList.AddRange(uiSecondCharacterStatusList.characterStatusList);
    }

    public void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }

    public void InitializeCharacterStatusList()
    {
        var uiCharacterList = InGameManager.instance.draggableCentral.uiCharacterArea.GetUICharacterListWithCharacters();

        for (int characterIndex = 0; characterIndex < InGameService.MAX_NUMBER_OF_CHARACTER_STATUS; ++characterIndex)
        {
            if (uiCharacterList.Count > characterIndex)
            {
                var character = uiCharacterList[characterIndex].character;
                characterStatusList[characterIndex].SetCharacterStatus(character);
            }
            else
            {
                characterStatusList[characterIndex].HideAll();
            }
        }
    }
}
