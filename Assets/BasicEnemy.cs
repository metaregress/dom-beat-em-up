using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;
        currentHealth = maxHealth;        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
