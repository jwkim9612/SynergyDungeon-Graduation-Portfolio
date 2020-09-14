using UnityEngine;

public class UIControl : MonoBehaviour
{
    // 화면에 표시
    public virtual void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    // 화면에서 숨김
    public virtual void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
