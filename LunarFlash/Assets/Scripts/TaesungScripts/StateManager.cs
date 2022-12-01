using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    //public AudioClip walking;
    //public AudioClip attacking;
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);

        }
    }
    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
