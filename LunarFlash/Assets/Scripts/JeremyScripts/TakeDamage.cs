using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeDamage : MonoBehaviour
{
    public float health = 50f;
    public TMP_Text score;
    public int currentsScore;

    private void Start()
    {
        currentsScore = 0;
    }

    public void hitDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            currentsScore += 100;
            score.text = "Current Score: " + currentsScore.ToString();
            Killed();
        }
    }
    
    void Killed()
    {
        Destroy(gameObject);
    }
   
}
