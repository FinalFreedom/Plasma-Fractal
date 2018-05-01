using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrainMap : PerlinNoise {
    public TerrainColour[] colours;
    public float deep = 56;
    public int[] area = { 256, 256 };
    public float zoom = 4;
    public int recursions = 4;
    public float consistency = 0.25f;
    public float roughness = 4f;
    // Use this for initialization
    void Start () {
        depth = deep;
        width = area[0];
        height = area[1];
        scale = zoom;
        octaves = recursions; //+ve
        persistance = consistency; // 0 to 1
        lacunarity = roughness;
        setUp();
        Terrain data = GetComponent<Terrain>();
        
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
