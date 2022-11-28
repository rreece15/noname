using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomSpawn : MonoBehaviour
{
    [SerializeField] private int numToSpawn;
    [SerializeField] private GameObject obPrefab;
    private List<GameObject> obs = new List<GameObject>();

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        obs.Add(Instantiate(obPrefab, new Vector3(380, 2, 600), Quaternion.identity)); // this is ONLY for inventory testing - will get deleted
        obs.Add(Instantiate(obPrefab, new Vector3(380, 2, 560), Quaternion.identity));// this is ONLY for inventory testing - will get deleted
        obs.Add(Instantiate(obPrefab, new Vector3(380, 2, 580), Quaternion.identity));// this is ONLY for inventory testing - will get deleted
        spawn();
    }

    void spawn() //spawns randomly in a specific range from zero
    {
        for(int i = 0; i < numToSpawn; i++)
        {
            obs.Add(Instantiate(obPrefab, new Vector3(Random.Range(0, 1000), 5, Random.Range(0, 1000)), Quaternion.identity));
        }
    }
}

