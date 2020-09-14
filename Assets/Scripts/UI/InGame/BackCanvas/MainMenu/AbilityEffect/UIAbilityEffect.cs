using UnityEngine;
using UnityEngine.UI;

public class UIAbilityEffect : MonoBehaviour
{
    [SerializeField] private Image effectImage = null;
    [SerializeField] private Text remainingTurnText = null;
    [SerializeField] protected Toggle toggle = null;
    [SerializeField] private UIAbilityEffectInfo uiAbilityEffectInfo = null;

    public AbilityEffect abilityEffect;

    public void Initialize()
    {
        SetToggle();
    }

    private void SetToggle()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((bool bOn) =>
        {
            if (bOn)
            {
                uiAbilityEffectInfo.SetAbilityEffectInfo(abilityEffect);
                uiAbilityEffectInfo.OnShow();
            }
        });
    }

    public void UpdateAbilityEffect()
    {
        UpdateRemainingTurnText();
    }

    /// <summary>
    /// 효과의 남은 턴을 하나 줄이고 업데이트
    /// </summary>
    public void UpdateAbilityEffectByWaveComplete()
    {
        abilityEffect.DecreaseRemainingTurn();
        UpdateRemainingTurnText();
    }

    public void SetabilityEffect(PotionData potionData)
    {
        effectImage.sprite = potionData.Image;
        SetRemainingTurnText(AbilityEffectService.NUM_OF_INFINITY);

        abilityEffect = new AbilityEffect(potionData);
    }

    private void SetRemainingTurnText(int remainingTurn)
    {
        if (remainingTurn == AbilityEffectService.NUM_OF_INFINITY)
        {
            remainingTurnText.text = "∞";
        }
        else
        {
            remainingTurnText.text = $"{remainingTurn}";
        }
    }

    public void UpdateRemainingTurnText()
    {
        if (abilityEffect.remainingTurn == AbilityEffectService.NUM_OF_INFINITY)
        {
            remainingTurnText.text = "∞";
        }
        else
        {
            remainingTurnText.text = $"{abilityEffect.remainingTurn}";
        }
    }

    public bool IsOver()
    {
        return abilityEffect.IsOver();
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }
}
