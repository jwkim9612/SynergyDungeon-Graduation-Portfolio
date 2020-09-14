using UnityEngine;
using UnityEngine.UI;

public class UILevelAndExp : MonoBehaviour
{
    [SerializeField] private UIAddExp uiAddExp = null;
    [SerializeField] private UILevel uiLevel = null;
    [SerializeField] private UIExpBar uiExpBar = null;

    public void Initialize()
    {
        uiAddExp.Initialize();
        uiLevel.Initialize();
        uiExpBar.Initialize();
    }
}
