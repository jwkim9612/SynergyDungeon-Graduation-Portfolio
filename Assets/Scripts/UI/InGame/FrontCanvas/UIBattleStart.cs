using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleStart : MonoBehaviour
{
    [SerializeField] private Text textBattleStart = null;

    public float playTime;

    public void PlayAnimation()
    {
        OnShow();
        StartCoroutine(Co_PlayAnimation());
    }

    private IEnumerator Co_PlayAnimation()
    {
        float defaultSize = textBattleStart.transform.localScale.x;
        float time = 0.0001f;
        float timePerSection = playTime / 3;

        while(time <= timePerSection)
        {
            textBattleStart.transform.localScale = Vector3.one * (defaultSize / (defaultSize / timePerSection * time));
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(timePerSection);
        
        while (time >= 0)
        {
            textBattleStart.transform.localScale = Vector3.one * (defaultSize / (defaultSize / timePerSection * time));
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        OnHide();
    }

    private void OnShow()
    {
        gameObject.SetActive(true);
    }

    private void OnHide()
    {
        gameObject.SetActive(false);
    }
}
