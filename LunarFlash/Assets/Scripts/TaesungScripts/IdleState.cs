using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public ChaseState chaseState;
    public EnterSphere enterSphere;
   // public bool canSeeThePlayer;
    public Transform Enemy;
     public int speed;
     public Transform[] waypoints;
     private int waypointIndex;
     private float dist; 

    void Start()
    {
         waypointIndex = 0;
        Enemy.transform.LookAt(waypoints[waypointIndex].position);
    }
    void Update()
    {
    }
    public override State RunCurrentState()
    {
        if (enterSphere.canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
       
        dist = Vector3.Distance(Enemy.transform.position, waypoints[waypointIndex].position);
        if(dist < 1f)
        {
            IncreaseIndex();
        }
        Enemy.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            return this;
        }

    }
    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        
        }
        Enemy.transform.LookAt(waypoints[waypointIndex].position);
    
}
}
