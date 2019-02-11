using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 1;

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
