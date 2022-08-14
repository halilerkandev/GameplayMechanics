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
    private bool isOnIsland = true;
    private bool isSmashed = false;

    public float jumpSpeed;
    public float distance;

    private Coroutine PowerupCoroutine;

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

        if (hasPowerup && powerupTag == "Powerup3" && Input.GetKeyDown(KeyCode.Space))
        {
            Smash();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") ||
            other.CompareTag("Powerup2") ||
            other.CompareTag("Powerup3"))
        {
            if(PowerupCoroutine != null)
            {
                StopCoroutine(PowerupCoroutine);
            }
            hasPowerup = true;
            powerupTag = other.tag;
            powerupIndicator.SetActive(hasPowerup);
            Destroy(other.gameObject);
            PowerupCoroutine = StartCoroutine(PowerupCountdownRoutine());
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

        if (collision.gameObject.CompareTag("Island") && !isOnIsland)
        {
            isOnIsland = true;
        }

        if (isOnIsland && isSmashed)
        {
            isSmashed = false;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    Vector3 awayFromPlayer = enemies[i].transform.position - transform.position;

                    if(awayFromPlayer.magnitude < distance)
                    {
                        Rigidbody enemyRb = enemies[i].GetComponent<Rigidbody>();
                        enemyRb.AddForce(2 * _powerupStrength * awayFromPlayer, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    private void Fire()
    {
        isFiring = true;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Vector3 lookDirection = (enemies[i].transform.position - transform.position).normalized;
                Instantiate(projectile, transform.position + lookDirection, Quaternion.Euler(lookDirection));
            }
        }

        if (hasPowerup && powerupTag == "Powerup2" && enemies.Length > 0)
        {
            Invoke(nameof(Fire), 0.5f);
        }
        else
        {
            isFiring = false;
        }
    }

    private void Smash()
    {
        if (isOnIsland)
        {
            _playerRb.AddForce(jumpSpeed * Vector3.up, ForceMode.Impulse);
            StartCoroutine(SmashRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(hasPowerup);
    }

    IEnumerator SmashRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        _playerRb.velocity = Vector3.zero;
        _playerRb.AddForce(2 * jumpSpeed * Vector3.down, ForceMode.Impulse);
        isSmashed = true;
    }
}
