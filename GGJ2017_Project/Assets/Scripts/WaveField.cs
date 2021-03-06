﻿using UnityEngine;
using System.Collections;

public class WaveField : MonoBehaviour {

    // Terrain object
    Terrain terrain;

    TerrainData data { get { return terrain.terrainData; } }

    // Resource acquisition
    void Awake()
    {
        terrain = GetComponent<Terrain>();
    }

    void Start()
    {
        data.heightmapResolution = 129;
        ResetHeightmap();
    }
	
    void OnTriggerStay(Collider other)
    {
        Wave waveObject = other.gameObject.GetComponent<Wave>();
        if(waveObject != null)
        {
            float PI_2 = Mathf.PI * 2;
            float stepSize = 0.05f;

            float totalRadius = waveObject.waveCollider.radius * 0.75f;

            float[,] heightmap = data.GetHeights(0, 0, data.heightmapWidth, data.heightmapHeight);

            int waveFrames = Mathf.Min(waveObject.framesAlive, 16);

            float normalizedLife = Mathf.Clamp01(Mathf.Log(waveObject.life) + 1.1f) / waveObject.initialLife;

            for (int i = 1; i <= waveFrames; i++)
            {
                float percentageOfFrames = ((float)i / (float)waveFrames);
                float currentRadius = percentageOfFrames * totalRadius;

                float waveHeight = 0.5f + (0.5f * Mathf.Cos(percentageOfFrames * PI_2) * normalizedLife);

                for (float theta = 0; theta < PI_2; theta += stepSize)
                {
                    float x = currentRadius * Mathf.Cos(theta);
                    float z = currentRadius * Mathf.Sin(theta);

                    int[] coord = WorldspaceToHeightmapCoord(waveObject.transform.position + new Vector3(x,0,z));

                    if (coord[1] < data.heightmapWidth && coord[0] < data.heightmapHeight
                        &&
                        coord[1] >= 0 && coord[0] >= 0)
                    {
                        heightmap[coord[1], coord[0]] = waveHeight;
                    }
                }
            }

            terrain.terrainData.SetHeightsDelayLOD(0,0,heightmap);
        }
    }

    void OnApplicationQuit()
    {
        ResetHeightmap();
    }

    public void ResetHeightmap()
    {
        float[,] heightmap = data.GetHeights(0, 0, data.heightmapWidth, data.heightmapHeight);

        for (int i = 0; i < heightmap.GetLength(0); i++)
        {
            for (int j = 0; j < heightmap.GetLength(1); j++)
            {
                heightmap[i, j] = 0.5f;
            }
        }

        data.SetHeightsDelayLOD(0, 0, heightmap);
    }

    public int[] WorldspaceToHeightmapCoord(Vector3 worldspaceCoord)
    {
        Vector3 tempCoord = (worldspaceCoord - transform.position);

        Vector3 coord;

        coord.x = tempCoord.x / terrain.terrainData.size.x;
        coord.z = tempCoord.z / terrain.terrainData.size.z;

        int[] finalCoord = { (int)(coord.x * terrain.terrainData.heightmapWidth), (int)(coord.z * terrain.terrainData.heightmapHeight) };

        return finalCoord;
        
    }
}
