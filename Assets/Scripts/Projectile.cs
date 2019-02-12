using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    int damage = 0;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public void GiveNewDamage(int newDamge)
    {
        damage = newDamge;
    }
}
