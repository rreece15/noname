using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int numEnemies;
    public int score;
    List<GameObject> enemies = new List<GameObject>();
    public GameObject EnemyPrefab;

    private bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectsWithTag("Landscape")[0].GetComponent<Landscape>().makeTerrain();
        //SpawnEnemies();
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // spawns one enemy every 5 seconds in the nightTime
        if (!GameObject.FindGameObjectsWithTag("timer")[0].GetComponent<DayTimer>().isDay() && !waiting)
        {

            Debug.Log("SpawningEnemy");
            SpawnEnemy();
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        waiting = true;
        yield return new WaitForSeconds(5);
        waiting = false;
    }

    void SpawnEnemy()
    {
        int nextX = Random.Range(0, 1025);
        int nextZ = Random.Range(0, 1025);
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
}
