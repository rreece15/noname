using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeDamage : MonoBehaviour
{
    public float health = 50f;
    public TMP_Text score;
    public int currentsScore;
    public Animator animator;

    private void Start()
    {
        currentsScore = 0;
    }

    public void hitDamage (float amount)
    {
        health -= amount;
        StartCoroutine(Damaged());
        if (health <= 0f)
        {
            currentsScore += 100;
            score.text = "Current Score: " + currentsScore.ToString();
            StartCoroutine(Killed());
        }
    }
    
    IEnumerator Killed()
    {
        
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator Damaged()
    {
        animator.SetTrigger("Take_Damage_1");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Walk_Cycle_1");
    }
   
}
