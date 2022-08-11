using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float _spawnXRange = 8.0f;
    private float _spawnZRange = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 GenerateRandomPosition()
    {
        float randomXPos = Random.Range(-_spawnXRange, _spawnXRange);
        float randomZPos = Random.Range(-_spawnZRange, _spawnZRange);

        Vector3 randomPos = new(randomXPos, 0, randomZPos);

        return randomPos;
    }
}
