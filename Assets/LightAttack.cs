using UnityEngine;
using System.Collections;


public class LightAttack : Move
{

    public int damage = 10;

    // Use this for initialization
    void Start()
    {
        startup = 6;
        active = 3;
        recovery = 5;

    stateMachine = GetComponent<AttackStateMachine>();
        stateMachine.SetNewState(AttackStateMachine.States.Startup, startup);
        stateMachine.runTimer = true;

        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
        visualHitBox = GetComponent<SpriteRenderer>();
        visualHitBox.enabled = false;

        hitbox.enabled = false;

        moveName = "Light Attack";
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    // Function called when a move begins its active frames
    override protected void OnActive()
    {
        hitbox.enabled = true;
        if (debugHitbox)
        {
            visualHitBox.enabled = true;
        }
    }

    override protected void OnRecovery()
    {
        hitbox.enabled = false;
        if (debugHitbox)
        {
            visualHitBox.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.currentHealth -= damage;
        }
    }
}
