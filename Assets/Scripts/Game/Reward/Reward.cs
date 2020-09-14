public class Reward
{
    public RewardCurrency rewardCurrency { get; set; }
    public int amount { get; set; }
    public int id { get; set; }

    public Reward()
    {
        rewardCurrency = RewardCurrency.None;
        amount = 0;
        id = 0;
    }

    public Reward(RewardCurrency rewardCurrency, int amount, int id)
    {
        this.rewardCurrency = rewardCurrency;
        this.amount = amount;
        this.id = id;
    }

    public Reward(RewardCurrency rewardCurrency, int amount)
    {
        this.rewardCurrency = rewardCurrency;
        this.amount = amount;
        this.id = 0;
    }
}
