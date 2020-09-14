using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFloatingTextForInGame : UIFloatingText
{
    public override void Initialize()
    {
        canvas = GameObject.Find("FrontCanvas").GetComponent<Transform>();
        originPosition = transform.localPosition;
        isCoroutineRunning = false;
    }

    protected override IEnumerator UpdateText()
    {
        isCoroutineRunning = true;

        var originParent = this.transform.parent;
        this.transform.SetParent(canvas.transform);
        transform.SetAsFirstSibling();

        Vector3 localPosition = this.transform.localPosition;
        this.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0.0f);

        float runningTime = 0.0f;

        while (runningTime < duration)
        {
            yield return new WaitForEndOfFrame();
            
            Color currentColor = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f - runningTime / duration); // Fade Out
            
            transform.Translate(new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f));
            runningTime += Time.deltaTime;
        }

        OnHide();
        transform.SetParent(originParent);
        transform.localPosition = originPosition;
        isCoroutineRunning = false;
        StopCoroutine(updateCoroutine);
    }
}
