using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIStartScreen : MonoBehaviour
{
    [SerializeField] private GameObject loading = null;
    [SerializeField] private Image fadeImage = null;
    [SerializeField] private float FadeTime = 0.0f;

    private void Start()
    {
        InitialzeFadeImage();
        LoadSceneManager.Instance.OnLoading += ShowLoading;
        PlayFadeIn();
    }

    private void ShowLoading()
    {
        loading.gameObject.SetActive(true);
    }

    private void InitialzeFadeImage()
    {
        Color DefaultFadeColor = fadeImage.color;
        DefaultFadeColor.a = 1.0f;

        fadeImage.color = DefaultFadeColor;
    }

    private void PlayFadeIn()
    {
        StartCoroutine(Co_PlayFadeIn());
    }

    private IEnumerator Co_PlayFadeIn()
    {
        float time = 0.0f;
        float endAlpha = 0.0f;
        Color fadeColor = fadeImage.color;

        while(fadeImage.color.a > 0.1f)
        {
            time += Time.deltaTime / FadeTime;
            fadeColor.a = Mathf.Lerp(fadeColor.a, endAlpha, time);
            fadeImage.color = fadeColor;
            yield return new WaitForEndOfFrame();
        }

        fadeColor.a = endAlpha;
        fadeImage.color = fadeColor;

        HideFadeImage();
    }

    private void HideFadeImage()
    {
        fadeImage.gameObject.SetActive(false);
    }
}
