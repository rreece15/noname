using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyDataScriptableObject", order =1)]
public class EnemyData : ScriptableObject 
{
    [Header("Editor Values")]
    public string enemyName;
    public int destpoints;//Destination points - random destpoints on map & randomly assign points to each enemy
    public GameObject possibleDest;
    public int possibleDestCount;

    public EnemyType enemyType;
    
    public float idleTime = 3.0f;
    public float attackTime = 5f;
    public float attackDistance;
    public float fullHealth;
    public float explosionHealth;
    public float distanceFromPlayer;
    public float explosionDamage;
   

    public GameObject bullet;
    public NavMeshAgent enemy;
    [Space]
    [Header("Runtime Values")]
    public float attackTimer;
    public float switchTimer;
    public float enemyHP;
    public int _possibleDestCount;
    public bool patrolStarted = false;


    public void SetUpEnemey()
    {
        switchTimer = idleTime;
        attackTimer = attackTime;
        enemyHP = fullHealth;
        _possibleDestCount = possibleDestCount;
    }
    public void EnemyIdle(Transform currentLoc)
    {
        patrolStarted = false;
        switchTimer -= Time.deltaTime;
        enemy.destination = currentLoc.position;
        //Do nothing, stay where it currently located for idleTime 
    }

    public void ChasePlayer(Transform player)
    {
      
        // Detect Player **
        // Close to the player & maintain the distanceFromPlayer 
            enemy.destination = new Vector3((player.position.x +distanceFromPlayer),(player.position.y), (player.position.z+distanceFromPlayer));
        //AttackPlayer();
    }

    public void AttackPlayer(GameObject enemyobject, Transform target) // refer to this script https://pastebin.com/ZXAPEsrk
    {
        attackTimer-=Time.deltaTime;
        if (attackTimer <= 0)
        {
             enemyobject.transform.LookAt(target);
            GameObject newBullet = Instantiate(bullet, enemyobject.transform.GetChild(3).transform.position, enemyobject.transform.rotation);
            newBullet.GetComponent<Bullet_Enemy>().enemybullet.SetUpEnemyBullet();

           
             Vector3 shootDirection = enemyobject.transform.forward;

            if (WhetherHasComponent<Rigidbody>(newBullet))
            {
                newBullet.GetComponent<Rigidbody>().velocity = shootDirection * 20f;
            }

            attackTimer = attackTime;
        }
        // Attack player - shoot a bullet

    }

    public void Explode(Transform player, GameObject currentEnemy)
    {

        if(enemyHP <= explosionHealth)
        {
            enemy.speed = 10f;
            enemy.destination = new Vector3((player.position.x/*+ distanceFromPlayer)*/), (player.position.y), (player.position.z/* + distanceFromPlayer)*/));     
        }
    }

    public void ExplodeStart(GameObject player, GameObject currentEnemy)
    {
        
        player.GetComponent<Player>().DamageFromEnemyAttack(currentEnemy.GetComponent<EnemyControl>().enemy1.explosionDamage);
        Destroy(currentEnemy);
    }

    public void Patrol(Transform[] dests)//, Transform currentLoc)//, Transform currentPos)
    {
        if (patrolStarted == false)
        {
           var next = Random.Range(0, destpoints);
         
            enemy.destination = dests[next].position;
            patrolStarted = true;
        }
   
  
    }

    public static bool WhetherHasComponent<T>(GameObject obj) where T : Component //from this post https://stackoverflow.com/questions/35166730/how-to-check-if-a-game-object-has-a-component-method-in-unity
    {
        return obj.GetComponent<T>() != null;
    }
}

public enum EnemyType
{
    Enemy1,
    Enemy2
}


