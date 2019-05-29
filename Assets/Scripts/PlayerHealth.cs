using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;

    float maxHealth = 100;
    float curHealth;

    void Start()
    {
        
        curHealth = healthBar.value;
        curHealth = maxHealth;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //curHealth -= damage;
        }
    }

    void Update()
    {
        healthText.text = curHealth.ToString();
        curHealth = healthBar.value;
    }
}
