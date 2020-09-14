public class RunePurchaseableLevelData
{
    public RunePurchaseableLevelData(RunePurchaseableLevelExcelData runePurchaseableLevelExcelData)
    {
        SalesId = runePurchaseableLevelExcelData.SalesId;
        PurchaseableLevel = runePurchaseableLevelExcelData.PurchaseableLevel;
    }

    public RunePurchaseableLevelData(RunePurchaseableLevelData runePurchaseableLevelData)
    {
        SalesId = runePurchaseableLevelData.SalesId;
        PurchaseableLevel = runePurchaseableLevelData.PurchaseableLevel;
    }

    public int SalesId;
    public int PurchaseableLevel;
}
