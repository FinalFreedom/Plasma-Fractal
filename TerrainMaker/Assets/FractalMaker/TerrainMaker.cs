using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrainMaker : MonoBehaviour {
    public int mapSize;
    private Plasma heightmap;
    public Terrain map;
	// Use this for initialization
	void Start () {
        heightmap = new Plasma();
        heightmap.setUp(mapSize, 0.5f, 0.05f, 0.5f, 0.5f, 0.000000001f);
        map.terrainData.SetHeightsDelayLOD(0, 0, heightmap.getHeightMap());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
