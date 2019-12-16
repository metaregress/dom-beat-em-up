using UnityEngine;
using System.Collections;

public class AttackStateMachine : MonoBehaviour
{
    public enum States
    {
        Startup,
        Active,
        Recovery,
        None
    }

    public States currentState;
    public int timeLeftInState; // measured in frames
    public bool runTimer; // should we keep counting

    private void Update()
    {
        if (runTimer)
        {
            timeLeftInState -= 1;
        }
    }

    public void SetNewState(States newState, int newTime)
    {
        currentState = newState;
        timeLeftInState = newTime;
    }

}
