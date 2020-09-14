using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class UIHitText : UIFloatingText
{
    //[SerializeField] private Transform canvas = null;
    //[SerializeField] private float moveSpeed = 0.0f;
    //[SerializeField] private float duration = 0.0f;
    //[SerializeField] private Text damageText = null;
    //private Vector3 originPosition;
    //private Coroutine updateCoroutine;

    //public void Initialize()
    //{
    //    canvas = GameObject.Find("Canvas").GetComponent<Transform>();
    //    originPosition = transform.localPosition;
    //}

    ///// <summary>
    ///// HitText를 실행
    ///// </summary>
    ///// <param name="damage"> 받은 데미지 </param>
    //public void Play()
    //{
    //    this.gameObject.SetActive(true);
    //    updateCoroutine = StartCoroutine(UpdateText());
    //}

    //public void SetDamageText(string text)
    //{
    //    damageText.text = text;
    //}

    //private IEnumerator UpdateText()
    //{
    //    var originParent = this.transform.parent;

    //    this.transform.SetParent(canvas.transform);
    //    float runningTime = 0.0f;

    //    while (true)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        this.transform.Translate(new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f));
    //        runningTime += Time.deltaTime;

    //        if (runningTime >= duration)
    //        {
    //            this.gameObject.SetActive(false);
    //            this.transform.SetParent(originParent);
    //            this.transform.localPosition = originPosition;
    //            StopCoroutine(updateCoroutine);
    //        }
    //    }
    //}
}
