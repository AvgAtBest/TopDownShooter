using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float bulletSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        DestroyBullet();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);

            gameObject.SetActive(false);
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject, 100);
    }
    
}
