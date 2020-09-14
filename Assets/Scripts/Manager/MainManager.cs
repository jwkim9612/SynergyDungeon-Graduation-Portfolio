using GameSparks.Api.Requests;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance = null;

    public BackCanvas backCanvas;
    public FrontCanvas frontCanvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UIManager.Instance.SetCanEscape(true);

        backCanvas.Initialize();
        frontCanvas.Initialize();

        SaveManager.Instance.CheckHasInGameData();
    }
}
