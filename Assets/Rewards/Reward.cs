using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpaceDefenders/Reward"))]
public class Reward : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] string text;
    [SerializeField] Type type;

    public enum Type
    {
        damage,
        movementSpeed,
        firingRate,
        projectileSpeed,
        health
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public string GetString()
    {
        return text;
    }

    public Type GetRewardType()
    {
        return type;
    }
}
