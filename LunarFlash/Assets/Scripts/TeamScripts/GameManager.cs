using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int numEnemies;
    public int score;
    public int enemyHealth;
    bool cleared;
    int waveNum;
    // public int playerHealth;  PLEASE USE playerScript.GetPlayerHP(); method to get playerHP value! 
    List<GameObject> enemies = new List<GameObject>();
    public GameObject EnemyPrefab;
    AudioSource gmAudio;
    private bool waiting;
    public Player playerScript;

    public int mapWidth;
    public int mapDepth;
    // Start is called before the first frame update
    void Start()
    {
        cleared = false;
        waveNum = 0;
        if (Instance == null)
        { Instance = this; }
        GameObject.FindGameObjectsWithTag("Landscape")[0].GetComponent<Landscape>().makeTerrain();
        //SpawnEnemies();
        waiting = false;
        gmAudio = this.GetComponent<AudioSource>();
        //gmAudio.Play();

        if (playerScript == null)
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // spawns one enemy every 5 seconds in the nightTime
        if (!GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<DayTimer>().isDay() && !waiting)
        {
            GameObject.FindGameObjectsWithTag("Wave")[0].GetComponent<TMP_Text>().SetText("");
            Debug.Log("SpawningEnemy");
            cleared = false;
            SpawnEnemy();
            StartCoroutine(waiter());
        }
        if (GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<DayTimer>().getDayTime() > 0.95 && !cleared)
        {
            //clearEnemies();
            cleared = true;
            waveNum++;
            //Debug.Log(waveNum);
            // show wave num
            GameObject.FindGameObjectsWithTag("Wave")[0].GetComponent<TMP_Text>().SetText("Wave " + waveNum + " Complete!");
        }
        else if (!cleared && waveNum >= 3)
        {
            //Debug.Log("player win");
            Player.isGameClear = true;
        }

        /////////////////when players clear all enemy waves - then Player.isGameClear = true
    }

    IEnumerator waiter()
    {
        waiting = true;
        yield return new WaitForSeconds(5);
        waiting = false;
    }

    void SpawnEnemy()
    {
        int nextX = Random.Range(0, mapWidth);
        int nextZ = Random.Range(0, mapDepth);
        RaycastHit hit;

        if(Physics.Raycast(new Ray(new Vector3(nextX, 100, nextZ), Vector3.down), out hit, Mathf.Infinity, -1))
        {
            if(hit.collider != null)
            {
                Debug.Log(hit.point);
                Vector3 v = new Vector3(nextX, hit.point.y + 2, nextZ);
                Debug.DrawRay(new Vector3(nextX, 100, nextZ), Vector3.down, Color.white, 100f, false);
                enemies.Add(Instantiate(EnemyPrefab, v, Quaternion.identity));
            }
        }
    }

    public void clearEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }

    public float GetPlayerHPInfo()
    {
        return playerScript.GetPlayerHP();
    }
}
