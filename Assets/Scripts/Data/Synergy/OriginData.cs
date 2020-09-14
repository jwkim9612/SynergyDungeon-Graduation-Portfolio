using UnityEngine;

public class OriginData
{
    public OriginData(OriginExcelData originExcelData)
    {
        Origin = originExcelData.Name;
        Description = originExcelData.Description;

        Image = Resources.Load<Sprite>(originExcelData.ImagePath);
    }

    public OriginData(OriginData originData)
    {
        Origin = originData.Origin;
        Description = originData.Description;
        Image = originData.Image;
    }

    public Origin Origin;
    public string Description;
    public Sprite Image;
}
