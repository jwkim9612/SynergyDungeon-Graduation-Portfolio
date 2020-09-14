using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFloatingText : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 0.0f;
    [SerializeField] protected float duration = 0.0f;
    [SerializeField] private Text text = null;
    protected Transform canvas = null;

    protected Coroutine updateCoroutine;
    protected Vector3 originPosition;
    protected bool isCoroutineRunning;

    public virtual void Initialize()
    {
        canvas = transform.root;
        originPosition = transform.localPosition;
        isCoroutineRunning = false;
    }

    public void Play()
    {
        OnShow();

        if(isCoroutineRunning)
        {
            StopCoroutine(updateCoroutine);
            transform.localPosition = originPosition;
        }

        updateCoroutine = StartCoroutine(UpdateText());
    }

    public void SetText(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetTextSize(int size)
    {
        text.fontSize = size;
    }

    protected virtual IEnumerator UpdateText()
    {
        isCoroutineRunning = true;

        var originParent = this.transform.parent;
        this.transform.SetParent(canvas.transform);
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

    protected void OnShow()
    {
        gameObject.SetActive(true);
    }

    protected void OnHide()
    {
        gameObject.SetActive(false);
    }
}
