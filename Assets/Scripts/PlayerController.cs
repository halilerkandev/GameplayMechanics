using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody _playerRb;
    private GameObject _focalPoint;
    public bool hasPowerup;
    private float _powerupStrength = 20.0f;
    public GameObject powerupIndicator;
    private string powerupTag;

    public GameObject projectile;

    private GameObject[] enemies;

    private bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float forwardInput = Input.GetAxis("Vertical");

        _playerRb.AddForce(forwardInput * speed * _focalPoint.transform.forward);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (hasPowerup && powerupTag == "Powerup2" && enemies.Length > 0 && !isFiring)
        {
            Fire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") || other.CompareTag("Powerup2"))
        {
            hasPowerup = true;
            powerupTag = other.tag;
            powerupIndicator.SetActive(hasPowerup);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup && powerupTag == "Powerup")
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log("Collided with " + collision.gameObject.name + " with " + powerupTag + " powerup set to " + hasPowerup);

            enemyRb.AddForce(_powerupStrength * awayFromPlayer, ForceMode.Impulse);
        }
    }

    private void Fire()
    {
        isFiring = true;

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 lookDirection = (enemies[i].transform.position - transform.position).normalized;
            Instantiate(projectile, transform.position + lookDirection, Quaternion.Euler(lookDirection));
        }

        if (hasPowerup && powerupTag == "Powerup2" && enemies.Length > 0)
        {
            Invoke(nameof(Fire), 0.5f);
        } else
        {
            isFiring = false;
        }

        Debug.Log("Fire!");
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(hasPowerup);
    }
}
