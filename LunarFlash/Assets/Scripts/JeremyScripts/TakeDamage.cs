using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health = 50f;

    public void hitDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Killed();
        }
    }
    
    void Killed()
    {
        Destroy(gameObject);
    }
   
}
