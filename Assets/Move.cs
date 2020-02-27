using UnityEngine;
using System.Collections;

public abstract class Move : MonoBehaviour
{
    public string moveName;

    public BoxCollider2D hitbox;
    public SpriteRenderer visualHitBox;

    public bool debugHitbox;

    public AttackStateMachine stateMachine;

    public int startup;
    public int active;
    public int recovery;    

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stateMachine.timeLeftInState <= 0)
        {
            // We've finished our startup frames, time to go active!
            if (stateMachine.currentState == AttackStateMachine.States.Startup)
            {
                stateMachine.SetNewState(AttackStateMachine.States.Active, active);
                OnActive();
            }
            // We've finished our active frames, time to go into recovery
            else if (stateMachine.currentState == AttackStateMachine.States.Active)
            {
                stateMachine.SetNewState(AttackStateMachine.States.Recovery, recovery);
                OnRecovery();
            }
            // If we're done recovering, shut down the state machine in a None state to indicate the move isn't in use
            else if (stateMachine.currentState == AttackStateMachine.States.Recovery)
            {
                stateMachine.SetNewState(AttackStateMachine.States.None, 1);
                stateMachine.runTimer = false;
            }
        }
    }

    protected abstract void OnActive();

    protected abstract void OnRecovery();

}
