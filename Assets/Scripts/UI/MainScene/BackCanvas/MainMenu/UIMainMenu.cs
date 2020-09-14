using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public SimpleScrollSnap simpleScrollSnap;

    public UIIllustratedBook uiIllustratedBook;
    public UIBattlefield uiBattlefield;
    public UIStore uiStore;

    public void Initialize()
    {
        uiIllustratedBook.Initialize();
        uiStore.Initialize();
        uiBattlefield.Initialize();
    }
}
