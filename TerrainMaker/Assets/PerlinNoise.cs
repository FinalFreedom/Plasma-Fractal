using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {
    public float depth = 0;
    public int width = 256;
    public int height = 256;
    public float scale = 20;
    public int octaves = 4; //+ve
    public float persistance = 0.5f; // 0 to 1
    public float lacunarity = 2; //Greater than 1
    public float offsetX;
    public float offsetY;
    // Use this for initialization
    void Start () {
        float offsetX = (Random.value - 0.5f) * 100;
        float offsetY = (Random.value - 0.5f) * 100;
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = MakeTerrain(terrain.terrainData);
	}

    protected TerrainData MakeTerrain(TerrainData data)
    {
        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, depth, height);
        data.SetHeights(0, 0, MakeHeights());
        return data;
    }
	float[,] MakeHeights()
    {
        float max = float.MinValue;
        float min = float.MaxValue;
        float[,] heightmap = new float[width, height];

        for(int x = 0; x<width;x++)
        {

            for(int y = 0; y<height;y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for(int i = 0; i<octaves; i++)
                {
                    float xCord = ((float)x / width) * scale * frequency;
                    float yCord = ((float)y / height) * scale * frequency;
                    float point = Mathf.PerlinNoise(xCord + offsetX, yCord + offsetY) * 2 - 1;
                    noiseHeight += point * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if(noiseHeight<min)
                {
                    min = noiseHeight;
                }
                if(noiseHeight>max)
                {
                    max = noiseHeight;
                }
                heightmap[x, y] = noiseHeight;
            }
        }
        for(int x = 0; x<width;x++)
        {
            for(int y = 0; y<height;y++)
            {
                heightmap[x, y] = Mathf.InverseLerp(min, max, heightmap[x, y]);
            }
        }
        return heightmap;
    }
	// Update is called once per frame
	void Update () {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = MakeTerrain(terrain.terrainData);
    }
    void OnValidate()
    {
        if(octaves < 1)
        {
            octaves = 1;
        }
        Mathf.Clamp(persistance, 0, 1);
        if(lacunarity <1)
        {
            lacunarity = 1;
        }
    }
}
