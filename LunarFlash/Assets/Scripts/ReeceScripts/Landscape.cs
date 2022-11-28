using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Landscape : MonoBehaviour
{

    public int width;
    public int height;
    public int variation;

    public float scale = 20f;

    public float xOffset;
    public float yOffset;

    public NavMeshSurface surface;
    bool terrainDone;

    // Start is called before the first frame update
    void Start()
    {
        terrainDone = false;
        xOffset = Random.Range(0f, 999f);
        yOffset = Random.Range(0f, 999f);
        makeTerrain();
        surface = gameObject.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = TerrainMaker(terrain.terrainData);
        terrainDone = true;
        surface.BuildNavMesh();
    }

    TerrainData TerrainMaker(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, variation, height);
        terrainData.SetHeights(0, 0, PerlinHeights());
        return terrainData;
    }

    float[,] PerlinHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = Mathf.PerlinNoise((float)x / width * scale + xOffset, (float)y / height * scale + yOffset);
            }
        }
        return heights;
    }

    public bool getDone()
    {
        return terrainDone;
    }
}
