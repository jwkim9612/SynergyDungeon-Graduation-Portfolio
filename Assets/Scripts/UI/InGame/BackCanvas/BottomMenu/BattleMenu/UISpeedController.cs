using UnityEngine;
using UnityEngine.UI;

public class UISpeedController : MonoBehaviour
{
    [SerializeField] private Text speedText = null;
    [SerializeField] private Button changeSpeedButton = null;
    public float currentSpeed;

    public void Initialize()
    {
        ChangeToDefaultSpeed();

        SetChangeSpeedButton();

        InGameManager.instance.gameState.OnPrepare += ChangeToDefaultSpeed;
    }

    private void SetChangeSpeedButton()
    {
        changeSpeedButton.onClick.AddListener(() =>
        {
            ChangeSpeed();
        });
    }

    public void ChangeSpeed()
    {
        if(currentSpeed == InGameService.DEFAULT_SPEED)
        {
            currentSpeed = InGameService.DOUBLE_SPEED;
            EnemyService.SetDoubleSpeed();
        }
        else
        {
            currentSpeed = InGameService.DEFAULT_SPEED;
            EnemyService.SetDefaultSpeed();
        }

        ChangeText();
        ChangeTimeScale();
    }

    public void ChangeText()
    {
        if (currentSpeed == InGameService.DEFAULT_SPEED)
        {
            speedText.text = "X1";
        }
        else
        {
            speedText.text = "X2";
        }
    }
    
    public void ChangeToDefaultSpeed()
    {
        currentSpeed = InGameService.DEFAULT_SPEED;
        ChangeText();
        ChangeTimeScale();
    }

    public void ChangeTimeScale()
    {
        Time.timeScale = currentSpeed;
    }
}
