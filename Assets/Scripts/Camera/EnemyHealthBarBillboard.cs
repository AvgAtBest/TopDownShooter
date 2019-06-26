using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarBillboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.LookAt(Camera.main.transform);
		//have the health bar look at the main Camera
    }
}
