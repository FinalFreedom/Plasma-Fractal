using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoise : PerlinNoise {
    bool waxingWave;
	// Use this for initialization
	void Start () {
        scale = 1;
        waxingWave = false;
	}
	
	// Update is called once per frame
    void Update () {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = MakeTerrain(terrain.terrainData);
        if(waxingWave)
        {
            depth += Time.deltaTime;
        }
        else
        {
            depth -= Time.deltaTime;
        }
        if(depth<1 || depth>10)
        {
            waxingWave = !waxingWave;
        }
        offsetX += Time.deltaTime/5;
    }
}
