using UnityEngine;
using UnityEngine.UI;

public class UIObtainedGoods : MonoBehaviour
{
    [SerializeField] private Image goodsImage = null;
    [SerializeField] private Text goodsName = null;

    public virtual void SetUIObtainedGoods(int Id)
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
}
