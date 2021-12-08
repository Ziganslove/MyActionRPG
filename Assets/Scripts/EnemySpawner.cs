using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private Transform[] spawnPoints;
    private float lastSpawnTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            SpawnAtPoint(enemyPrefab, i);
            lastSpawnTime = -2.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Spider.enemyCount < 5)
        {
            SpawnAtPoint(enemyPrefab, 1);
        }
    }

    public void SpawnAtPoint(GameObject enemy, int point)
    {
        if (Time.time - lastSpawnTime > 2.0f)
        {
            Spider.enemyCount++;
            Instantiate(enemy, spawnPoints[point].position, Quaternion.identity);
            lastSpawnTime = Time.time;
        }
    }
}
