using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //매니저들
    public static GameManager instance = null;
    public ParticleService particleService = null;


    //파괴되지 않는 싱글턴
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

        // 다른 씬으로 이동해도 소멸되지 않음
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIManager.Instance.Initialize();
        SoundManager.Instance.Initialize();
        AccountManager.Instance.Initialize();
        JsonDataManager.Instance.Initialize();
        SaveManager.Instance.Initialize();
        DataBase.Instance.Initialize();
    }

    public void LoadGameAndLoadMainScene()
    {
        StartCoroutine(Co_LoadGameAndLoadMainScene());
    }

    private IEnumerator Co_LoadGameAndLoadMainScene()
    {
        ServiceManager.Instance.Initialize();
        PlayerDataManager.Instance.Initialize();
        TimeManager.Instance.Initialize();
        GoodsManager.Instance.Initialize();
        StageManager.Instance.Initialize();

        /////////// 게임스파크에서 초당 10개 이상의 요청을 받을 시 오류가 발생하기 때문에 1초 텀을 두었음.
        yield return new WaitForSeconds(1.0f);
        RuneManager.Instance.Initialize();
        yield return new WaitForSeconds(1.0f);
        PotionManager.Instance.Initialize();

        LoadSceneManager.Instance.LoadMainScene();
    }

    public void LoadGameAndLoadInGameScene()
    {
        StartCoroutine(Co_LoadGameAndLoadInGameScene());
    }

    private IEnumerator Co_LoadGameAndLoadInGameScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("InGameScene");
    }

    public void Quit()
    {
        // 에디터인 경우 play모드를 false로
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        // 어플리케이션 종료
        #else
            Application.Quit();
        #endif

    }
}
