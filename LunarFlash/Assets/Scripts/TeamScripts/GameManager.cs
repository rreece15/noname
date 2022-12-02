using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int numEnemies;
    public int score;
    public TMP_Text scoreBoard;
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

    public GameObject[] ObjectPrefab;
    public GameObject gameLightObject;
    Light gameLight;
    DayTimer gameLightController;
    List<GameObject> objects = new List<GameObject>();
    TMP_Text waveNotif;
    bool waveNotifOn = false;

    
    // Start is called before the first frame update
    void Start()
    {
        cleared = false;
        waveNum = 1;//waveNum = 0;
        if (Instance == null)
        { Instance = this; }
        GameObject.FindGameObjectsWithTag("Landscape")[0].GetComponent<Landscape>().makeTerrain();
        //SpawnEnemies();
        waiting = false;
        gmAudio = this.GetComponent<AudioSource>();
        //gmAudio.Play();
        score = 0;
        scoreBoard.text = "Current Score: " + score.ToString();
        if (playerScript == null)
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() ;
        }
        waveNotifOn = true;
        waveNotif = GameObject.FindGameObjectsWithTag("Wave")[0].GetComponent<TMP_Text>();
        //LightSetting();
        gameLight = GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<Light>();
    }

    void LightSetting()
    {
        Instantiate(gameLightObject);
        gameLight = gameLightObject.GetComponent<Light>();
        if (gameLight != null)
        { gameLight.intensity = 0; }
        gameLightController = gameLightObject.GetComponent<DayTimer>();

    }

    public void ResetLighIntentisy()
    {
        gameLight.intensity = 0;
    }
    // Update is called once per frame
    void Update()
    {
        // spawns one enemy every 5 seconds in the nightTime
        if ((!GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<DayTimer>().isDay() && !waiting))
        {
            if (waveNotifOn == true)
            {
                waveNotifOn = false;
                StartCoroutine(WaveStartAlert());
                SpawnObjects();
                GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<randomSpawn>().spawn(waveNum);
            }
            //waveNotif.SetText("   Wave " + waveNum + " !!");
            Debug.Log("SpawningEnemy");
            cleared = false;
            SpawnEnemy();
            StartCoroutine(waiter(waveNum));
           // SpawnObjects();
        }
        if (GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<DayTimer>().getDayTime() > 0.95 && !cleared)
        {
            clearObjects();
            //clearEnemies();
            cleared = true;
           
            //Debug.Log(waveNum);
            // show wave num
            StartCoroutine(SetUpWaveNotification()); 
            waveNum++;
            //GameObject.FindGameObjectsWithTag("Wave")[0].GetComponent<TMP_Text>().SetText("Wave " + waveNum + " Complete!");
          //  GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<randomSpawn>().spawn(waveNum);
            waveNotifOn = true;
        }
        else if (/*!*/cleared && waveNum /*>*/== 4)
        {
            //Debug.Log("player win");
            clearEnemies();
            Player.isGameClear = true;
        }

        /////////////////when players clear all enemy waves - then Player.isGameClear = true
    }

    IEnumerator WaveStartAlert()
    {
        //if (waveNum == 0)
      //  { waveNotif.SetText(" !!  Wave 1 !!"); }
       // else
        //{
            waveNotif.SetText(" !!  Wave " + waveNum +" !!");
       // }
        
            
            yield return new WaitForSeconds(2f);
        waveNotif.SetText("");

    }
    IEnumerator SetUpWaveNotification()
    {
        waveNotif.SetText("Wave " + waveNum + " Complete!");

        yield return new WaitForSeconds(3f);

        waveNotif.SetText("Next enemy wave is coming soon! ");

        yield return new WaitForSeconds(2f);

        waveNotif.SetText("Good Luck..! ");

        yield return new WaitForSeconds(2f);

        waveNotif.SetText("");


    }
    public void updateScore()
    {
        score += 100;
        scoreBoard.text = "Current Score: " + score.ToString();
    }

    public int GetFinalScore()
    {
        return score;
    }

    IEnumerator waiter(int wave)
    {
        waiting = true;
        if (waveNum == 1)
        { yield return new WaitForSeconds(5); }
        else if (waveNum == 2)
        { yield return new WaitForSeconds(3); }
        else if (waveNum == 3)
        { yield return new WaitForSeconds(1); }
        waiting = false;
    }

    void SpawnEnemy()
    {
        int nextX = Random.Range(mapWidth/5, mapWidth*4/5);//0, mapWidth);
        int nextZ = Random.Range(mapDepth/5, mapDepth*4/5);//0, mapDepth);
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

    public void RemoveEnemyFromEnemyList(GameObject killedEnemey)
    {
        enemies.Remove(killedEnemey);
    }
    public void clearEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }

    void SpawnObjects()
    {
        var howManyObjects = Random.Range(3, 10);
        for (int i = 0; i < howManyObjects; i++)
        {
            int nextX = Random.Range(mapWidth / 4, mapWidth * 3 / 4);//0, mapWidth);
            int nextZ = Random.Range(mapDepth / 4, mapDepth * 3 / 4);//0, mapDepth);
            RaycastHit hit;

            if (Physics.Raycast(new Ray(new Vector3(nextX, 100, nextZ), Vector3.down), out hit, Mathf.Infinity, -1))
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.point);
                    Vector3 v = new Vector3(nextX, hit.point.y + 2, nextZ);
                    Debug.DrawRay(new Vector3(nextX, 100, nextZ), Vector3.down, Color.white, 100f, false);

                        var objectIndex = Random.Range(0, ObjectPrefab.Length);
                        objects.Add(Instantiate(ObjectPrefab[objectIndex], v, Quaternion.identity));
                
                }
            }
        }
    }

    public void clearObjects()
    {
        foreach(GameObject obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }
    

    public float GetPlayerHPInfo()
    {
        return playerScript.GetPlayerHP();
    }
}
