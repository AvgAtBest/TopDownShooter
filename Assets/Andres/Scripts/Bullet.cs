using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;

    void Start()
    {
            
    }
    void Update()
    {
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
