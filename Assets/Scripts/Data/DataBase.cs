public class DataBase : MonoSingleton<DataBase>
{
    public CharacterDataSheet characterDataSheet;
	public TribeDataSheet tribeDataSheet;
	public OriginDataSheet originDataSheet;
	public ChapterInfoDataSheet chapterInfoDataSheet;
	public ChapterDataSheet chapterDataSheet;
	public CharacterAbilityDataSheet characterAbilityDataSheet;
	public EnemyDataSheet enemyDataSheet;
	public ProbabilityDataSheet probabilityDataSheet;
	public InGameExpDataSheet inGameExpDataSheet;
	public PlayerExpDataSheet playerExpDataSheet;
	public RuneDataSheet runeDataSheet;
	public PotionDataSheet potionDataSheet;
	public GoodsDataSheet goodsDataSheet;
	public RunePurchaseableLevelDataSheet runePurchaseableLevelDataSheet;
	public RuneCombinableNumDataSheet runeCombinableNumDataSheet;

	public void Initialize()
    {
		CheckValidate();

		tribeDataSheet.Initialize();
		originDataSheet.Initialize();
		characterDataSheet.Initialize();
		characterAbilityDataSheet.Initialize();
		chapterInfoDataSheet.Initialize();
		chapterDataSheet.Initialize();
		enemyDataSheet.Initialize();
		probabilityDataSheet.Initialize();
		runeDataSheet.Initialize();
		potionDataSheet.Initialize();
		goodsDataSheet.Initialize();
		runePurchaseableLevelDataSheet.Initialize();
		runeCombinableNumDataSheet.Initialize();
		inGameExpDataSheet.Initialize();
		playerExpDataSheet.Initialize();
	}

	public void CheckValidate()
	{
		tribeDataSheet.DataValidate();
		originDataSheet.DataValidate();
		characterDataSheet.DataValidate();
		characterAbilityDataSheet.DataValidate();
		chapterInfoDataSheet.DataValidate();
		chapterDataSheet.DataValidate();
		enemyDataSheet.DataValidate();
		probabilityDataSheet.DataValidate();
		runeDataSheet.DataValidate();
		potionDataSheet.DataValidate();
		goodsDataSheet.DataValidate();
		runePurchaseableLevelDataSheet.DataValidate();
		runeCombinableNumDataSheet.DataValidate();
		inGameExpDataSheet.DataValidate();
		playerExpDataSheet.DataValidate();
	}
}
