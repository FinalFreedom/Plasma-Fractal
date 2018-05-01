using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoise : PerlinNoise {
    bool waxingWave;
	// Use this for initialization
	void Start () {
        depth = 0.2f;
        width = 256;
        height = 256;
        scale = 40;
        octaves = 4; //+ve
        persistance = 0.25f; // 0 to 1
        lacunarity = 4;
        setUp();
        waxingWave = false;
	}
	
	// Update is called once per frame
    void Update () {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = MakeTerrain(terrain.terrainData);
        if(waxingWave)
        {
            depth += Time.deltaTime/1000;
        }
        else
        {
            depth -= Time.deltaTime/1000;
        }
        if(depth<0.1 || depth>0.4)
        {
            waxingWave = !waxingWave;
        }
        offsetX += Time.deltaTime;
    }
}
