using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody _enemyRb;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;

        _enemyRb.AddForce(lookDirection * speed, ForceMode.Acceleration);

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
