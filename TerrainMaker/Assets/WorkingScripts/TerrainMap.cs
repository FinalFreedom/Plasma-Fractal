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

    public override float pointModifier(int xCord, int yCord)
    {
        //float x = (xCord/width) - 0.5f;
        //float y = (yCord/height)- 0.5f;

        //
        float x = (Mathf.InverseLerp(0, width, xCord)-0.5f);
        float y = (Mathf.InverseLerp(0, height, yCord)-0.5f);
        return -Mathf.Pow(x*3,2) - Mathf.Pow(y*3, 2);
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
