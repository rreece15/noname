using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomSpawn : MonoBehaviour
{
    int numToSpawn;
    public GameObject obPrefab;
    private List<GameObject> obs = new List<GameObject>();

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    void spawn() //spawns randomly in a specific range from zero
    {
        for(int i = 0; i < numToSpawn; i++)
        {
            obs.Add(Instantiate(obPrefab, new Vector3(Random.Range(0, 50), 5, Random.Range(0, 50)), Quaternion.identity));
        }
    }
}

