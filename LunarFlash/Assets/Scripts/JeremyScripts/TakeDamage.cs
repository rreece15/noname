using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeDamage : MonoBehaviour
{
    public float health = 50f;
    //public TMP_Text score;
    //public int currentsScore;
    public Animator animator;
    GameManager addScore;

    private void Start()
    {
        //currentsScore = 0;
        addScore = FindObjectOfType<GameManager>();
    }

    public void hitDamage (float amount)
    {
        health -= amount;
        StartCoroutine(Damaged());
        if (health <= 0f)
        {
            
            StartCoroutine(Killed());
        }
    }
    
    IEnumerator Killed()
    {
        
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        //currentsScore += 100;
        //score.text = "Current Score: " + currentsScore.ToString();
        addScore.updateScore();
        addScore.RemoveEnemyFromEnemyList(gameObject);
        Destroy(gameObject.transform.parent.gameObject);
    }

    IEnumerator Damaged()
    {
        animator.SetTrigger("Take_Damage_1");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Walk_Cycle_1");
    }
   
}
