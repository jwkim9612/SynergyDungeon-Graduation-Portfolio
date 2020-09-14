using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardList : IEnumerable<Reward>
{
    private List<Reward> list;

    public RewardList()
    {
        list = new List<Reward>();
    }

    /// <summary>
    /// ID가 없는 보상을 추가할 때
    /// </summary>
    /// <param name="rewardCurrency"></param>
    /// <param name="amount"></param>
    public void AddReward(RewardCurrency rewardCurrency, int amount)
    {
        if(rewardCurrency != RewardCurrency.Gold &&
           rewardCurrency != RewardCurrency.Diamond &&
           rewardCurrency != RewardCurrency.Exp)
        {
            Debug.Log("Error AddReward. please enter gold or diamond");
            return;
        }

        var reward = list.Find(x => x.rewardCurrency == rewardCurrency);
        if(reward == null)
        {
            list.Add(new Reward(rewardCurrency, amount));
        }
        else
        {
            reward.amount += amount;
        }
    }

    public void AddReward(RewardCurrency rewardCurrency, int amount, int id)
    {
        var reward = list.Find(x => x.rewardCurrency == rewardCurrency);
        if (reward.id == 0)
        {
            list.Add(new Reward(rewardCurrency, amount, id));
        }
        else
        {
            reward.amount += amount;
        }
    }

    public IEnumerator<Reward> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
