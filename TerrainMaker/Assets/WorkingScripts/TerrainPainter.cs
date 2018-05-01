using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPainter : MonoBehaviour {
    Terrain map;
    public indexHeight[] textures;
	// Use this for initialization
	void Start () {
        map = GetComponent<Terrain>();
        for(int x = 0; x<map.terrainData.heightmapWidth; x++)
        {
            for(int y = 0; y<map.terrainData.heightmapHeight; y++)
            {
                float point = map.terrainData.GetHeight(x, y);
                for(int i = 0; i<textures.Length;i++)
                {
                    if(point>textures[i].heightRequirement)
                    {
                        //map.terrainData.set
                    }
                }
            }
        }
        Renderer pic = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public struct indexHeight
{
    public int textureIndex;
    public float heightRequirement;
    private float modheight;
    public float getHeight()
    {
        modheight = heightRequirement;
        modheight += Random.Range(-0.01f,0.01f);
        return modheight;
    }

}