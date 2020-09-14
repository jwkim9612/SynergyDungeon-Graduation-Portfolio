public class ProbabilityData
{
    public ProbabilityData(ProbabilityExcelData probabilityExcelData)
    {
        Level = probabilityExcelData.Level;
        OneTier = probabilityExcelData.OneTier;
        TwoTier = probabilityExcelData.TwoTier;
        ThreeTier = probabilityExcelData.ThreeTier;
        FourTier = probabilityExcelData.FourTier;
    }

    public ProbabilityData(ProbabilityData probabilityData)
    {
        Level = probabilityData.Level;
        OneTier = probabilityData.OneTier;
        TwoTier = probabilityData.TwoTier;
        ThreeTier = probabilityData.ThreeTier;
        FourTier = probabilityData.FourTier;
    }

    public int Level;
    public int OneTier;
    public int TwoTier;
    public int ThreeTier;
    public int FourTier;
}
