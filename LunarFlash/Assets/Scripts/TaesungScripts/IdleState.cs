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
    AudioSource walkingSound;

    void Start()
    {
         waypointIndex = 0;
        Enemy.transform.LookAt(waypoints[waypointIndex].position);

        if(walkingSound == null)
        {
            walkingSound = this.gameObject.GetComponent<AudioSource>();
        }
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
        if(dist < 5f)
        {
            IncreaseIndex();
        }
        Enemy.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            walkingSound.Play();
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
