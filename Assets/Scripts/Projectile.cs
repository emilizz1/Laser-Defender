using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] int startingDamage = 1;

    int damage = 0;

    void Start()
    {
        damage = startingDamage;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public void IncreaseDamage()
    {
        damage += 1;
    }
}
