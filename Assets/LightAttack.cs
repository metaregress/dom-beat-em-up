using UnityEngine;
using System.Collections;


public class LightAttack : Move
{
    public int startup = 6;
    public int active = 3;
    public int recovery = 5;

    public int damage = 10;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        if (stateMachine.timeLeftInState <= 0)
        {
            // We've finished our startup frames, time to go active!
            if(stateMachine.currentState == AttackStateMachine.States.Startup)
            {
                stateMachine.SetNewState(AttackStateMachine.States.Active, active);
                hitbox.enabled = true;
                if(debugHitbox)
                {
                    visualHitBox.enabled = true;
                }
            }
            // We've finished our active frames, time to go into recovery
            else if(stateMachine.currentState == AttackStateMachine.States.Active)
            {
                stateMachine.SetNewState(AttackStateMachine.States.Recovery, recovery);
                hitbox.enabled = false;
                if (debugHitbox)
                {
                    visualHitBox.enabled =false;
                }
            }
            // If we're done recovering, shut down the state machine in a None state to indicate the move isn't in use
            else if (stateMachine.currentState == AttackStateMachine.States.Recovery)
            {
                stateMachine.SetNewState(AttackStateMachine.States.None, 1);
                stateMachine.runTimer = false;
            }
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
