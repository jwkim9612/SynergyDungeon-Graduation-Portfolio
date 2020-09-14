using UnityEngine;
using UnityEngine.UI;

public class UIWave : MonoBehaviour
{
    [SerializeField] private Text waveText = null;

    public void Initialize()
    {
        UpdateText();

        InGameManager.instance.gameState.OnPrepare += UpdateText;
    }

    public void UpdateText()
    {
        int wave = StageManager.Instance.currentWave;

        waveText.text = ("" + wave);
    }
}
