using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private UIControl exitUIControl = null;

    public bool isPause;
    private bool isInitialized;
    private bool canEscapeKey;

    //UI 기록
    private Stack<UIControl> uiHistory = new Stack<UIControl>();

    public void Initialize()
    {
        isPause = false;
        isInitialized = true;
        canEscapeKey = false;
    }

    //void Start()
    //{
    //    if(isInitialized)
    //    {
    //        return;
    //    }

    //    Debug.Log("Start uimainager");
    //    isInitialized = true;
    //    Initialize();
    //}

    public void SetCanEscape(bool escapable)
    {
        canEscapeKey = escapable;
    }

    public void ShowMessage(string _message)
    {
        //messageUI.ShowMessage(_message);
    }

    void Update()
    {
        if(canEscapeKey)
        {
            //Back키 입력 시 뒤로 가기
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideAndShowPreview();
            }
        }
    }

    //새로운 UIControl 추가 및 표시
    public void ShowNew(UIControl newUI)
    {
        newUI.OnShow();
        uiHistory.Push(newUI);
    }

    public void AddHistroy(UIControl newUI)
    {
        uiHistory.Push(newUI);
    }

    public void DeleteHistory()
    {
        uiHistory.Pop();
    }

    //이전 UIControl 숨김 후 새로운 UIControl 추가 및 표시
    public void HidePreviewAndShowNew(UIControl newUI)
    {
        if (uiHistory.Count != 0)
        {
            uiHistory.Peek().OnHide();
        }
        newUI.OnShow();
        uiHistory.Push(newUI);
    }

    //현재 UIControl 숨김 후 이전 UIControl 표시
    public void HideAndShowPreview()
    {
        if (uiHistory.Count != 0)
        {
            uiHistory.Pop().OnHide();

            //
            if (uiHistory.Count == 0)
            {
                if (isInGame())
                {
                    var currentSpeed = InGameManager.instance.backCanvas.uiBottomMenu.uiBattleMenu.uiSpeedController.currentSpeed;
                    Time.timeScale = currentSpeed;
                }
            }
            //
            
            if (uiHistory.Count != 0)
            {
                uiHistory.Peek().OnShow();
            }
        }
        else
        {
            //아무 UI도 표시 안되있을 경우 종료 UI 표시
            ShowNew(exitUIControl);

            if(isInGame())
            {
                isPause = true;
                Time.timeScale = InGameService.PAUSE_SPEED;
            }
        }
    }

    public bool isInGame()
    {
        if (InGameManager.instance != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
