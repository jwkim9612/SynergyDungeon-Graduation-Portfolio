using System;
using UnityEngine;
using UnityEngine.UI;

public class UIIllustratedBook : MonoBehaviour
{
    public UIDetailedSettings uiDetailedSettings;
    public UICharacterList uiCharacterList;
    public UIRunePage uiRunePage = null;
    public UIObtainedRuneScreen uiObtainedRuneByCombinationScreen = null;

    public void Initialize()
    {
        uiRunePage.Initialize();
        uiDetailedSettings.Initialize();
        uiCharacterList.Initialize();
    }
}
