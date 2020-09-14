using System;

[Serializable]
public class PotionExcelData
{
    public int Id;
    public string Name;
    public PotionGrade Grade;
    public Ability Ability;
    public WayOfCalculate WayOfIncrease;
    public int IncreaseValue;
    public string Description;
    public string ImagePath;
}
