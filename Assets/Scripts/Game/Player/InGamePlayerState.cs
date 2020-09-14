using UnityEngine;

public class InGamePlayerState : MonoBehaviour
{
    public delegate void OnCoinChangedDelegate();
    public delegate void OnBonusCoinChangedDelegate();
    public delegate void OnExpChangedDelegate();
    public delegate void OnLevelUpDelegate();
    public OnCoinChangedDelegate OnCoinChanged { get; set; }
    public OnBonusCoinChangedDelegate OnBonusCoinChanged { get; set; }
    public OnExpChangedDelegate OnExpChanged { get; set; }
    public OnLevelUpDelegate OnLevelUp { get; set; }
    
    public int coin { get; set; }
    public int level { get; set; }
    public int exp { get; set; }
    public int SatisfyExp { get; set; }
    public int numOfCanPlacedInBattleArea { get; set; }
    public int currentBonusCoin { get; set; }
    public RewardList rewardList { get; set; }

    public void Initialize()
    {
        rewardList = new RewardList();

        if (SaveManager.Instance.IsLoadedData)
        {
            InitializeByInGameSaveData();
        }
        else
        {
            InitializeByDefault();
        }

        currentBonusCoin = GetBonusCoin();

        InGameManager.instance.gameState.OnPrepare += IncreaseCoinByPrepare;
        InGameManager.instance.gameState.OnComplete += IncreaseExpByBattleWin;
        InGameManager.instance.gameState.OnComplete += IncreaseRewardsByBattleWin;
    }

    // 인게임 세이브데이터로 초기화
    private void InitializeByInGameSaveData()
    {
        coin = SaveManager.Instance.inGameSaveData.Coin;
        level = SaveManager.Instance.inGameSaveData.Level;
        
        var inGameExpDataSheet = DataBase.Instance.inGameExpDataSheet;
        if (inGameExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
        {
            SatisfyExp = satisfyExp;
        }

        exp = SaveManager.Instance.inGameSaveData.Exp;
        numOfCanPlacedInBattleArea = level;

        var currentChapter = StageManager.Instance.currentChapter;
        var currentWave = StageManager.Instance.currentWave;

        var chapterInfoDataSheet = DataBase.Instance.chapterInfoDataSheet;
        for (; currentWave >= 1; --currentWave)
        {
            if (chapterInfoDataSheet.TryGetChapterInfoClearExpReward(currentChapter, currentWave, out var expRewardValue))
            {
                rewardList.AddReward(RewardCurrency.Exp, expRewardValue);
            }
            if (chapterInfoDataSheet.TryGetChapterInfoClearGoldReward(currentChapter, currentWave, out var goldRewardValue))
            {
                rewardList.AddReward(RewardCurrency.Gold, goldRewardValue);
            }
        }
    }

    // 기본 값으로 초기화
    private void InitializeByDefault()
    {
        coin = InGameService.DEFAULT_COIN;
        level = InGameService.DEFAULT_LEVEL;

        var inGameExpDataSheet = DataBase.Instance.inGameExpDataSheet;
        if (inGameExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
        {
            SatisfyExp = satisfyExp;
        }

        exp = InGameService.DEFAULT_EXP;
        numOfCanPlacedInBattleArea = InGameService.DEFAULT_NUM_OF_CAN_PLACED_IN_BATTLEAREA;
    }

    // 코인 증가
    public void IncreaseCoin(int increaseValue)
    {
        coin = Mathf.Clamp(coin + increaseValue, InGameService.MIN_NUMBER_OF_COIN, InGameService.MAX_NUMBER_OF_COIN);

        // OnCoinChanged가 비어있는지 확인 후 실행
        OnCoinChanged?.Invoke();
        UpdateBonusCoin();
    }

    // 준비 상태에 의한 코인 증가
    public void IncreaseCoinByPrepare()
    {
        IncreaseCoinByBonus();

        int currentChapter = StageManager.Instance.currentChapter;
        int currentWave = StageManager.Instance.currentWave;

        var chapterInfoDataSheet = DataBase.Instance.chapterInfoDataSheet;
        if(chapterInfoDataSheet.TryGetChapterInfoGoldAmount(currentChapter, currentWave, out var goldAmount))
        {
            IncreaseCoin(goldAmount);
        }
    }

    public void IncreaseCoinByBonus()
    {
        int bonusCoin = GetBonusCoin();
        if (bonusCoin > 0)
        {
            IncreaseCoin(bonusCoin);
        }
    }

    // 코인 사용
    public void UseCoin(int usedValue)
    {
        coin = Mathf.Clamp(coin - usedValue, InGameService.MIN_NUMBER_OF_COIN, coin);
        OnCoinChanged();
        UpdateBonusCoin();
    }

    // 경험치 증가
    public void IncreaseExp(int increaseValue)
    {
        if (IsMaxLevel())
            return;

        exp += increaseValue;

        if(exp >= SatisfyExp)
        {
            level += 1;
            exp -= SatisfyExp;

            var inGameExpDataSheet = DataBase.Instance.inGameExpDataSheet;
            if (inGameExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
            {
                SatisfyExp = satisfyExp;
            }

            IncreaseNumOfCanPlacedInBattleArea();
            OnLevelUp();
        }

        OnExpChanged();
    }

    // 경험치 구매에 의한 경험치 증가
    public void IncreaseExpByAddExp()
    {
        UseCoin(InGameService.CAN_BUY_EXP);
        IncreaseExp(InGameService.CAN_BUY_EXP);
    }

    // 배틀 승리에 의한 경험치 증가
    public void IncreaseExpByBattleWin()
    {
        int currentChapter = StageManager.Instance.currentChapter;
        int currentWave = StageManager.Instance.currentWave;

        var chapterInfoDateSheet = DataBase.Instance.chapterInfoDataSheet;
        if (chapterInfoDateSheet.TryGetChapterInfoExpAmount(currentChapter, currentWave, out var expAmount))
        {
            IncreaseExp(expAmount);
        }
    }

    // 배틀공간에 배치할 수 있는 수를 증가
    public void IncreaseNumOfCanPlacedInBattleArea()
    {
        Mathf.Clamp(++numOfCanPlacedInBattleArea, InGameService.MIN_NUMBER_OF_CAN_PLACED, InGameService.MAX_NUMBER_OF_CAN_PLACED);
    }

    // 현재 최고레벨인지를 반환
    public bool IsMaxLevel()
    {
        int satisfyExp = InGameManager.instance.playerState.SatisfyExp;
        int maxLevelOfSatisfyExp = PlayerService.MAX_LEVEL_OF_SATISFY_EXP;

        return satisfyExp == maxLevelOfSatisfyExp ? true : false;
    }

    public int GetBonusCoin()
    {
        int bonusCoin = Mathf.Clamp((int)(coin * InGameService.CALCULATE_BONUS_VALUE), InGameService.MIN_NUMBER_OF_BONUS, InGameService.MAX_NUMBER_OF_BONUS);
        return bonusCoin;
    }

    public void UpdateBonusCoin()
    {
        int bonusCoin = GetBonusCoin();
        if (currentBonusCoin != bonusCoin)
        {
            currentBonusCoin = bonusCoin;
            OnBonusCoinChanged();
        }
    }

    public void GetRewards()
    {
        foreach (var reward in rewardList)
        {
            switch (reward.rewardCurrency)
            {
                case RewardCurrency.Gold:
                    PlayerDataManager.Instance.AddGold(reward.amount);
                    break;
                case RewardCurrency.Exp:
                    PlayerDataManager.Instance.AddExp(reward.amount);
                    break;
            }
        }
    }

    public void IncreaseRewardsByBattleWin()
    {
        var currentChapter = StageManager.Instance.currentChapter;
        var currentWave = StageManager.Instance.currentWave;

        if (!StageManager.Instance.IsFinalWave())
        {
            --currentWave;

            if (currentWave == 0)
                return;
        }

        var chapterInfoDataSheet = DataBase.Instance.chapterInfoDataSheet;
        if (chapterInfoDataSheet.TryGetChapterInfoClearExpReward(currentChapter, currentWave, out var expRewardValue))
        {
            rewardList.AddReward(RewardCurrency.Exp, expRewardValue);
        }
        if (chapterInfoDataSheet.TryGetChapterInfoClearGoldReward(currentChapter, currentWave, out var goldRewardValue))
        {
            rewardList.AddReward(RewardCurrency.Gold, goldRewardValue);
        }
    }
}
