using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public IdleState idleState;
    public EnterSphere enterSphere;
    public Transform Player;
    public NavMeshAgent navMeshAgent;

    public override State RunCurrentState()
    {
        if (!enterSphere.canSeeThePlayer)
        {
            return idleState;
        }
        else
        {
        navMeshAgent.destination = Player.position;
       return this;
        }
    }
    
}
