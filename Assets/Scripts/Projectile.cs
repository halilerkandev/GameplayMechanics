using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        lookDirection = (transform.position - player.transform.position).normalized;
        StartCoroutine(DestroyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(10 * lookDirection, ForceMode.Impulse);
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
