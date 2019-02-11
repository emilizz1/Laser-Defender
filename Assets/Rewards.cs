using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField] int[] rewardsThershold;

    int currentReward = 0;

    public void CheckForRewards(int score)
    {
        if(score >= rewardsThershold[currentReward])
        {
            RecieveRewards();
            currentReward++;
        }
    }

    void RecieveRewards()
    {

    }
}
