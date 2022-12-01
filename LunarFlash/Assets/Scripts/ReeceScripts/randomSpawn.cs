using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomSpawn : MonoBehaviour
{
    [SerializeField] private int numToSpawn;
    [SerializeField] private GameObject obPrefab;
    private List<GameObject> obs = new List<GameObject>();

    public float mapWidth;
    public float mapDepth;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //obs.Add(Instantiate(obPrefab, new Vector3(380, 4, 600), Quaternion.identity)); // this is ONLY for inventory testing - will get deleted
      //  obs.Add(Instantiate(obPrefab, new Vector3(380, 4, 560), Quaternion.identity));// this is ONLY for inventory testing - will get deleted
       // obs.Add(Instantiate(obPrefab, new Vector3(380, 4, 580), Quaternion.identity));// this is ONLY for inventory testing - will get deleted
       //// obs.Add(Instantiate(obPrefab, new Vector3(380, 4, 550), Quaternion.identity));// this is ONLY for inventory testing - will get deleted
        //obs.Add(Instantiate(obPrefab, new Vector3(380, 4, 570), Quaternion.identity));// this is ONLY for inventory testing - will get deleted
        spawn();
    }

    void spawn() //spawns randomly in a specific range from zero
    {
        for(int i = 0; i < numToSpawn; i++)
        {
            obs.Add(Instantiate(obPrefab, new Vector3(Random.Range(mapWidth / 4, mapWidth*3/4), 6, Random.Range(mapDepth / 4, mapDepth * 3 / 4)), Quaternion.identity));
        }
    }
}

