using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    [SerializeField] int[] rewardsThershold;
    [SerializeField] Image[] images;
    [SerializeField] Text[] text;
    [SerializeField] Reward[] rewards;
    
    int currentRewardThreshold = 0;
    int currentReward = 0;
    bool recievingRewards = false;

    void Update()
    {
        if (recievingRewards)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RecieveReward();
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if(currentReward > 0)
                {
                    currentReward--;
                }
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if(currentReward < images.Length - 1)
                {
                    currentReward++;
                }
            }
        }
    }

    public void CheckForRewards(int score)
    {
        if(score >= rewardsThershold[currentRewardThreshold])
        {
            PreprareRewards();
            currentRewardThreshold++;
            currentReward = 0;
            recievingRewards = true;
        }
    }

    void PreprareRewards()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = rewards[i].GetSprite();
            text[i].text = rewards[i].GetString();
        }
    }

    void RecieveReward()
    {
        recievingRewards = false;
        var player = FindObjectOfType<PlayerController>();
        switch (rewards[currentReward].GetRewardType())
        {
            case (Reward.Type.firingRate):
                player.UpgradeFiringRate();
                break;
            case (Reward.Type.damage):
                player.UpgradeDamage();
                break;
            case (Reward.Type.health):
                player.UpgradeHealth();
                break;
            case (Reward.Type.movementSpeed):
                player.UpgradeSpeed();
                break;
            case (Reward.Type.projectileSpeed):
                player.UpgradeProjectileSpeed();
                break;
        }
    }
}
