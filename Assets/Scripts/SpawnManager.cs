using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float _spawnXRange = 8.0f;
    private float _spawnZRange = 7.0f;

    public int enemyCount;
    public int enemyWave = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomXPos = Random.Range(-_spawnXRange, _spawnXRange);
        float randomZPos = Random.Range(-_spawnZRange, _spawnZRange);

        Vector3 randomPos = new(randomXPos, 0, randomZPos);

        return randomPos;
    }
}
