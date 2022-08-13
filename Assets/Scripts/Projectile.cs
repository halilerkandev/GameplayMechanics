using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(HandleDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(10 * transform.rotation.eulerAngles.normalized, ForceMode.Impulse);
    }

    private IEnumerator HandleDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
