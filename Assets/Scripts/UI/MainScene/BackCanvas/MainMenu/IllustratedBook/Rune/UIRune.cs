using UnityEngine;
using UnityEngine.UI;

public class UIRune : MonoBehaviour
{
    [SerializeField] protected Image runeImage = null;
    [SerializeField] protected Image originImage = null;

    public bool isEquippedRune { get; set; } = false;

    public Rune rune { get; set; } = null;

    public virtual void SetUIRune(RuneData newRuneData)
    {
        rune = new Rune();
        rune.SetRune(newRuneData);

        SetRuneImage(newRuneData.Image);

        var origin = SynergyService.GetOriginByRuneSocketPosition(newRuneData.SocketPosition);
        if(DataBase.Instance.originDataSheet.TryGetOriginImage(origin, out var originImage))
        {
            SetOriginImage(originImage);
        }
    }

    public void SetRuneImage(Sprite sprite)
    {
        runeImage.sprite = sprite;
    }

    public void SetOriginImage(Sprite sprite)
    {
        originImage.sprite = sprite;
    }
}
