using UnityEngine;
using System.Collections;

public class Enemybehaviour : MonoBehaviour
{
    [SerializeField] int health = 2;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float firingRate = 0.5f;
    [SerializeField] int scoreValue = 10;
    [SerializeField] int projectileDamage = 5;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip deathSound;

    private Score scores;

    void Start()
    {
        scores = GameObject.Find("Score").GetComponent<Score>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            StartCoroutine(HitBlink());
            if (health <= 0)
            {
                Destroy(gameObject);
                scores.ScorePoints(scoreValue);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
        }
    }

    void Fire()
    {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Projectile>().GiveNewDamage(projectileDamage);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Update()
    {
        float probability = Time.deltaTime * firingRate; // 0.2 * 0.75=0.15    0.2*0.65= 0.13
        if (Random.value < probability)
        {
            Fire();
        }
    }

    IEnumerator HitBlink()
    {
        print("Blink");
        var tempSprite = GetComponent<SpriteRenderer>();
        tempSprite.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        tempSprite.material.SetFloat("_FlashAmount", 0);
    }
}