using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private Slider afterImageSlider = null;
    public Pawn controllingPawn { get; set; }
    private Coroutine afterImageCoroutine;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += UpdateHPBar;
        InGameManager.instance.gameState.OnBattle += InitializeAfterImageSlider;
    }

    public void SetHeight(Sprite sprite)
    {

    }

    public void SetUpdateHPBarAndAfterImage()
    {
        controllingPawn.OnHit += UpdateHPBarAndAfterImage;
    }

    private void InitializeAfterImageSlider()
    {
        afterImageSlider.value = 1;
    }

    public void UpdateHPBarAndAfterImage()
    {
        UpdateHPBar();
        afterImageCoroutine = StartCoroutine(PlayAfterImage());
    }

    public void UpdateHPBar()
    {
        if(controllingPawn.IsActivated())
        {
            slider.value = controllingPawn.GetHPRatio();
        }
    }

    private IEnumerator PlayAfterImage()
    {

        float subValue = (afterImageSlider.value - controllingPawn.GetHPRatio()) / InGameService.RATE_AT_WHICH_AFTERIMAGES_DISAPPEAR ;

        while(true)
        {
            yield return new WaitForEndOfFrame();

            if (afterImageSlider.value <= controllingPawn.GetHPRatio())
            {
                StopCoroutine(afterImageCoroutine);
            }

            afterImageSlider.value -= subValue;
        }
    }

    public void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
