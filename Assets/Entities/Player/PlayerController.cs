﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 15.0f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float firingRate;
    [SerializeField] int health = 10;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip hitSound;

    float xmin;
    float xmax;
    float lastFireTime = 0;
    int currentDamage = 5;

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        FindObjectOfType<PlayerHealth>().UpdateHealth(health);
    }

    void Fire()
    {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Projectile>().GiveNewDamage(currentDamage);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time - lastFireTime > firingRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            health -= missile.GetDamage();
            FindObjectOfType<PlayerHealth>().UpdateHealth(health);
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Lost Screen");
        Destroy(gameObject);
    }

    public void UpgradeSpeed()
    {
        speed += 1;
    }

    public void UpgradeDamage()
    {
        currentDamage++;
    }

    public void UpgradeProjectileSpeed()
    {
        projectileSpeed += 1;
    }

    public void UpgradeFiringRate()
    {
        firingRate -= 0.04f;
    }

    public void UpgradeHealth()
    {
        health += 25;
        FindObjectOfType<PlayerHealth>().UpdateHealth(health);
    }
}