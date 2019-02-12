using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] float speed;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] int[] scoreThreshold;
    [SerializeField] GameObject[] enemiesToAdd;

    bool playing = true;

    private bool movingRight = true;
    private float xmin;
    private float xmax;
    int currentScoreThreshold = 0;
    
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x;
        xmax = rightmost.x;
        spawn();
        StartCoroutine(SpawnUntilFull());
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
    }

    void spawn()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    IEnumerator SpawnUntilFull()
    {
        while (playing)
        {
            Transform[] freePositions = GetEmptyPositions();
            if (freePositions.Length > 0)
            {
                Transform freePosition = freePositions[Random.Range(0, freePositions.Length)];
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], freePosition.transform.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = freePosition;
            }
            yield return new WaitForSeconds((7- GetEmptyPositions().Length) * 0.2f);
        }
    }

    Transform[] GetEmptyPositions()
    {
        List<Transform> emptyPositions = new List<Transform>();
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                emptyPositions.Add(childPositionGameObject);
            }
        }
        return emptyPositions.ToArray();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    public void CheckToAddEnemies(int score)
    {
        if(currentScoreThreshold != scoreThreshold.Length && scoreThreshold[currentScoreThreshold] <= score)
        {
            enemyPrefabs.Add(enemiesToAdd[currentScoreThreshold]);
            currentScoreThreshold++;
        }
    }
}