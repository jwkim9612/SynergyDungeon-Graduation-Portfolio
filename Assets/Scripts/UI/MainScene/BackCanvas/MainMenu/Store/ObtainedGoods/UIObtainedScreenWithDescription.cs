using UnityEngine;
using UnityEngine.UI;

public class UIObtainedScreenWithDescription : UIControl
{
    [SerializeField] private Image goodsImage = null;
    [SerializeField] private Text goodsName = null;
    [SerializeField] protected Text goodsGrade = null;
    [SerializeField] protected Text goodsDescription = null;

    public virtual void SetUIObtainedScreen(int Id)
    {

    }

    protected void SetGoodsImage(Sprite sprite)
    {
        goodsImage.sprite = sprite;
    }

    protected void SetGoodsName(string name)
    {
        goodsName.text = name;
    }

    protected void SetGoodsDescription(string description)
    {
        goodsDescription.text = description;
    }

}
