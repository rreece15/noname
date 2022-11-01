using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{

    public EnemyData enemy1;
    public EnemyState state;
    public Transform[] destPoints;
    private GameObject player;
    [SerializeField] private bool isPlayerDetected;
    public GameObject enemyCanvas;
    public TMP_Text enemyHP_text;
    public Slider enemyHP_bar;
    // Start is called before the first frame update


    void Start()
    {
        enemy1.SetUpEnemey(); //Set up this enemy gameobject based on the scriptable data

        if (enemyHP_text == null)
        {
            enemyHP_text = enemyCanvas.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
            enemyHP_text.text = enemy1.enemyHP.ToString();
        }

        if (enemyHP_bar == null)
        {
            enemyHP_bar = enemyCanvas.transform.GetChild(0).gameObject.GetComponent<Slider>();
            enemyHP_bar.maxValue = enemy1.fullHealth; 
            enemyHP_bar.value = enemy1.enemyHP;
        }


        if (player == null) // Set up player 
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        List<Transform> options = new List<Transform>(); // possible options for destinations
        for (int j = 0; j < enemy1.possibleDestCount; j++)
        {
            options.Add(enemy1.possibleDest.transform.GetChild(j).gameObject.transform);
        }

        destPoints = new Transform[enemy1.destpoints]; // pick up destinations for this enemy randomly
        for (int i = 0; i < destPoints.Length; i++)
        {
            var selected = Random.Range(0, enemy1._possibleDestCount);
            destPoints[i] = options[selected];
            options.RemoveAt(selected);
            enemy1._possibleDestCount--;
        }

        var initialPos = Random.Range(0, enemy1.destpoints);
        this.gameObject.transform.position = destPoints[initialPos].position;

        if (enemy1.enemy == null) //NavMestAgent set up
        { enemy1.enemy = this.gameObject.GetComponent<NavMeshAgent>(); }

       

        state = EnemyState.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state) // refer to this script https://pastebin.com/qm2gM8Aw
        {
            case EnemyState.Idle:
                enemy1.EnemyIdle(this.gameObject.transform);
                break;
            case EnemyState.Chase:
                enemy1.ChasePlayer(player.transform);
                enemy1.AttackPlayer(this.gameObject, player.transform);
                break;
            case EnemyState.Patrol:
                enemy1.Patrol(destPoints);
                break;
            case EnemyState.Explode:
                enemy1.Explode(player.transform, this.gameObject);
                break;
            default:
                break;
        }
        ChangeState();

        if (enemy1.enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeState()
    {
        switch (state)
        {
            case EnemyState.Idle:
                if (enemy1.switchTimer < 0)
                    state = EnemyState.Patrol;
                if (isPlayerDetected)
                    state = EnemyState.Chase;
                break;
            case EnemyState.Chase:
                if (!isPlayerDetected)
                    state = EnemyState.Patrol;
                if (isPlayerDetected && enemy1.enemyHP <= enemy1.explosionHealth)
                    state = EnemyState.Explode; //Done
                break;
            case EnemyState.Patrol:
                if (isPlayerDetected)
                {
                    state = EnemyState.Chase;
                }
                if (CheckLocation(this.gameObject.transform))
                {
                    state = EnemyState.Idle;
                    enemy1.switchTimer = enemy1.idleTime;
                }

                break;
            default:
                break;
        }
    }

    public bool CheckLocation(Transform enemyPos)
    {
        if((enemyPos.position.x ==enemy1.enemy.destination.x)&& (enemyPos.position.z == enemy1.enemy.destination.z))
        {
            return true;
        }
        else { return false; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerDetected = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemy1.ExplodeStart(collision.gameObject, this.gameObject);
            collision.gameObject.GetComponent<Player>().ExplosionEffect();
        }
    }

    public void UpdateHP()
    {
        enemyHP_text.text = enemy1.enemyHP.ToString();
        enemyHP_bar.value = enemy1.enemyHP;
    }
}
public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Explode
}