using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrainMap : PerlinNoise {
    public TerrainColour[] colours;
    // Use this for initialization
    void Start () {
        depth = 56;
        width = 256;
        height = 256;
        scale = 4;
        octaves = 4; //+ve
        persistance = 0.25f; // 0 to 1
        lacunarity = 4;
        setUp();
    }
	public void updateVectors(float xPos, float yPos)
    {
        offsetX += xPos;
        offsetY += yPos;
        setUp();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
[System.Serializable]
public struct TerrainColour
{
    public string name;
    public float height;
    public Color colour;
}
