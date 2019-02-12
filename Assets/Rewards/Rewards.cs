using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    [SerializeField] GameObject rewardsUI;
    [SerializeField] int[] rewardsThershold;
    [SerializeField] Image[] images;
    [SerializeField] Text[] text;
    [SerializeField] GameObject[] arrowPosition;
    [SerializeField] GameObject arrow;
    [SerializeField] Reward[] rewards;
    
    int currentRewardThreshold = 0;
    int currentReward = 0;
    bool recievingRewards = false;
    int[] rewardNumbers = new int[3];

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
                    PositionArrow();
                }
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if(currentReward < images.Length - 1)
                {
                    currentReward++;
                    PositionArrow();
                }
            }
        }
    }

    void PositionArrow()
    {
        arrow.transform.position = arrowPosition[currentReward].transform.position;
    }

    public void CheckForRewards(int score)
    {
        if(score >= rewardsThershold[currentRewardThreshold])
        {
            
            rewardsUI.gameObject.SetActive(true);
            PreprareRewards();
            currentRewardThreshold++;
            currentReward = 0;
            recievingRewards = true;
            PositionArrow();
            Time.timeScale = 0f;
            if(currentRewardThreshold == rewardsThershold.Length)
            {
                FindObjectOfType<LevelManager>().LoadLevel("Win Screen");
            }
        }
    }

    void PreprareRewards()
    {
        for (int i = 0; i < images.Length; i++)
        {
            int rewardNumber = GetUniqueRewardNumber();
            rewardNumbers[i] = rewardNumber;
            images[i].sprite = rewards[rewardNumber].GetSprite();
            text[i].text = rewards[rewardNumber].GetString();
        }
    }

    int GetUniqueRewardNumber()
    {
        int rewardNumber = 0;
        bool lookingForUniqueNumber = true;
        while (lookingForUniqueNumber)
        {
            rewardNumber = Random.Range(0, rewards.Length);
            bool duplicate = false;
            foreach (int rewardNum in rewardNumbers)
            {
                if(rewardNum == rewardNumber)
                {
                    duplicate = true;
                }
            }
            if (!duplicate)
            {
                lookingForUniqueNumber = false;
            }
        }
        return rewardNumber;
    }

    void RecieveReward()
    {
        recievingRewards = false;
        var player = FindObjectOfType<PlayerController>();
        switch (rewards[rewardNumbers[currentReward]].GetRewardType())
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
        Time.timeScale = 1f;
        rewardsUI.gameObject.SetActive(false);
    }
}
