public class ExpData
{
    public ExpData(InGameExpExcelData inGameExpExcelData)
    {
        Level = inGameExpExcelData.Level;
        SatisfyExp = inGameExpExcelData.SatisfyExp;
        CumulativeExp = inGameExpExcelData.CumulativeExp;
    }

    public ExpData(PlayerExpExcelData playerExpExcelData)
    {
        Level = playerExpExcelData.Level;
        SatisfyExp = playerExpExcelData.SatisfyExp;
        CumulativeExp = playerExpExcelData.CumulativeExp;
    }

    public ExpData(ExpData inGameExpData)
    {
        Level = inGameExpData.Level;
        SatisfyExp = inGameExpData.SatisfyExp;
        CumulativeExp = inGameExpData.CumulativeExp;
    }

    public int Level;
    public int SatisfyExp;
    public int CumulativeExp;
}
