using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {
    protected float depth = 10;
    protected int width = 256;
    protected int height = 256;
    protected float scale = 20;
    protected int octaves = 4; //+ve
    protected float persistance = 0.5f; // 0 to 1
    protected float lacunarity = 2; //Greater than 1
    protected float offsetX;
    protected float offsetY;
    private float[,] gradientMap;
    private float[,] terrainHeights;
    // Use this for initialization
    void Start () {
        setUp();
	}
    protected void setUp()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = MakeTerrain(terrain.terrainData);
        MakeGradientMap(terrainHeights);
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
                /**if (x > (width / 2) - 25 & x < (width / 2) + 25)
                {
                    if (y > (height / 2) - 25 & y < (height / 2) + 25)
                    {
                        heightmap[x, y] = -1.0f;
                    }
                }**/
            }
        }
        for(int x = 0; x<width;x++)
        {
            for(int y = 0; y<height;y++)
            {
                heightmap[x, y] = Mathf.InverseLerp(min, max, heightmap[x, y]);
            }
        }
        terrainHeights = heightmap;
        return heightmap;
    }
    void MakeGradientMap(float[,] reference)
    {
        gradientMap = new float[width, height];
        for (int x=0; x<width; x++)
        {
            for(int y=0; y<height; y++)
            {
                int condition = 0;
                //Consider changing to switch x,y?
                if(x==0)
                {
                    if(y==0)
                    {
                        condition = 1; //Topleft
                    }
                    else if (y==height-1)
                    {
                        condition = 3; //Topright
                    }
                    else
                    {
                        condition = 2; //Top row
                    }
                }
                else if (x == width - 1)
                {
                    if (y == 0)
                    {
                        condition = 7; //Bottom left
                    }
                    else if (y == height - 1)
                    {
                        condition = 5; //Bottom right
                    }
                    else
                    {
                        condition = 6; //Bottom row
                    }
                }
                else if(y==0)
                {
                    condition = 8; //Leftmost
                }
                else if(y==height -1)
                {
                    condition = 5; //Rightmost
                }
                gradientMap[x, y] = GradientCalc(reference, condition, x, y);
            }
        }
    }
    float GradientCalc(float[,] map, int condition, int x, int y)
    {
        int x1 = x;
        int x2 = x;
        int y1 = y;
        int y2 = y;
        int divideX = 2;
        int divideY = 2;
        int divideD1 = 1;
        int divideD2 = 1;
        switch (condition)
        {
            case 0: //Non borderline index
                x1 = x - 1;
                x2 = x + 1;
                y1 = y - 1;
                y2 = y + 1;
                divideD1 = 2;
                divideD2 = 2;
                break;
            case 1: //Top left index
                x2 = x + 1;
                y2 = y + 1;
                divideX = 1;
                divideY = 1;
                break;
            case 2: //Top row index
                x2 = x + 1;
                y1 = y - 1;
                y2 = y + 1;
                divideX = 1;
                break;
            case 3: //Top right index
                x2 = x + 1;
                y1 = y - 1;
                divideX = 1;
                divideY = 1;
                break;
            case 4: //Rightmost index
                x1 = x - 1;
                y1 = y - 1;
                y2 = y + 1;
                divideX = 1;
                break;
            case 5: //Bottom right index
                x1 = x - 1;
                y1 = y - 1;
                divideX = 1;
                divideY = 1;
                break;
            case 6: //Bottom row index
                x1 = x - 1;
                y2 = y + 1;
                y1 = y - 1;
                divideX = 1;
                break;
            case 7: //Bottom left index
                x1 = x - 1;
                y1 = y + 1;
                divideX = 1;
                divideY = 1;
                break;
            case 8: //Left row index
                x2 = x + 1;
                x1 = x - 1;
                y2 = y + 1;
                divideY = 1;
                break;
        }
        float xGrad = Mathf.Abs((map[x1, y] + map[x2, y]) / divideX);
        float yGrad = Mathf.Abs((map[x, y1] + map[x, y2]) / divideY);
        float d1Grad = Mathf.Abs((map[x1, y1] + map[x2, y2]) / divideD1);
        float d2Grad = Mathf.Abs((map[x1, y2] + map[x2, y1]) / divideD2);
        return Mathf.Max(xGrad, yGrad, d1Grad, d2Grad);
    }
    public float[,] getHeightMap()
    {
        return terrainHeights;
        
    }
	// Update is called once per frame
	void Update () {
        //Terrain terrain = GetComponent<Terrain>();
        //terrain.terrainData = MakeTerrain(terrain.terrainData);
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
