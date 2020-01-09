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
        contactDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    public override int GetContactDamage()
    {
        return contactDamage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision detected");
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player)
        {
            player.currentHealth -= GetContactDamage();
        }
    }
}
