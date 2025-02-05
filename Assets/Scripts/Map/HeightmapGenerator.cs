using System;
using UnityEngine;

public class HeightmapGenerator : MonoBehaviour
{
    public float Tiling = 10.0f;
    public Terrain terrain;

    public float MaxClamp = 1;
    public float OceanLevel = 1;

    // Use this for initialization
    void Start ()
    {   
        GenerateHeights(terrain, Tiling);
    }
   
    // Update is called once per frame
    void Update ()
    {
        //GenerateHeights(terrain, Tiling);
    }

    public void GenerateHeights(Terrain terrain, float tileSize)
    {
        float[,] heights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

        for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
            {
                float perlin = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapResolution) * tileSize, ((float)k / (float)terrain.terrainData.heightmapResolution) * tileSize);
 
                heights[i, k] = Math.Clamp((float)Math.Pow(perlin, 2), 0, MaxClamp) / 10.0f;

                if(heights[i, k] < OceanLevel / 10.0f)
                {
                    heights[i, k] = (float)Math.Pow(heights[i, k] * 10f, 1.1) / 10f;
                }
            }
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
