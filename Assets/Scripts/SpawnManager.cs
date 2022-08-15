using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    public GameObject bossPrefab;

    private float _spawnXRange = 8.0f;
    private float _spawnZRange = 7.0f;

    public int enemyCount;
    public int enemyWave = 1;
    public int enemyWaveMax = 5;

    private bool _isBossSpawned = false;

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

        if (enemyCount == 0 && enemyWave == enemyWaveMax && !_isBossSpawned)
        {
            SpawnBoss();
            SpawnPowerup();
        }

        if (enemyCount == 0 && enemyWave < enemyWaveMax && !_isBossSpawned)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        GameObject powerupPrefab = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];
        Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateRandomPosition(), bossPrefab.transform.rotation);
        _isBossSpawned = true;
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomXPos = Random.Range(-_spawnXRange, _spawnXRange);
        float randomZPos = Random.Range(-_spawnZRange, _spawnZRange);
        Vector3 randomPos = new(randomXPos, 0, randomZPos);
        return randomPos;
    }
}
