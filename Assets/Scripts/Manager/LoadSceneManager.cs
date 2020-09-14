using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoSingleton<LoadSceneManager>
{
    public delegate void OnLoadingDelegate();
    public OnLoadingDelegate OnLoading { get; set; }

    [SerializeField] private Slider progressbar = null;
    [SerializeField] private Text loadText = null;

    public void LoadMainScene()
    {
        OnLoading();
        StartCoroutine(Co_LoadMainScene());
    }

    private IEnumerator Co_LoadMainScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");

        //로딩이 끝나도 멈추게 만듬.
        operation.allowSceneActivation = false;

        yield return new WaitForSeconds(0.5f);
        progressbar.value = 0.5f;
        UpdateLoadText();
        yield return new WaitForSeconds(1.0f);

        // 로딩이 끝나서 isDone이 true가 되기전까지 반복
        while (!operation.isDone)
        {
            yield return null;
            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if(operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1.0f, Time.deltaTime);
            }

            UpdateLoadText();
            
            if(progressbar.value >= 1.0f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }

    private void UpdateLoadText()
    {
        loadText.text = $"{Mathf.Floor(progressbar.value * 100)}%";
    }
}
