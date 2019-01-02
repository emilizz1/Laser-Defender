using UnityEngine;
using System.Collections;

public class Enemybehaviour : MonoBehaviour
{
    [SerializeField] float health = 150f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float firingRate = 0.5f;
    [SerializeField] int scoreValue = 10;
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
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Update()
    {
        float probability = Time.deltaTime * firingRate;
        if (Random.value < probability)
        {
            Fire();
        }
    }
}