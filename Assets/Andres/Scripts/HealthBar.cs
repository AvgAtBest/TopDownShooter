using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private static float value;
    public Slider healthbar;

    public float Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = Health;
    }
}
