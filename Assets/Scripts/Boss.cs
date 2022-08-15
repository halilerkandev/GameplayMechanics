using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] minionPrefabs;

    private float _spawnXRange = 8.0f;
    private float _spawnZRange = 7.0f;
    private float _spawnTimeRange = 5.0f;

    private Coroutine _spawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMinion()
    {
        GameObject minionPrefab = minionPrefabs[Random.Range(0, minionPrefabs.Length)];
        Instantiate(minionPrefab, GenerateRandomPosition(), minionPrefab.transform.rotation);
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_spawnTimeRange);
        SpawnMinion();
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomXPos = Random.Range(-_spawnXRange, _spawnXRange);
        float randomZPos = Random.Range(-_spawnZRange, _spawnZRange);
        Vector3 randomPos = new(randomXPos, 0, randomZPos);
        return randomPos;
    }
}
