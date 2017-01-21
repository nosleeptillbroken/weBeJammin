using UnityEngine;
using System.Collections;

public class WaveField : MonoBehaviour {

    // Terrain object
    Terrain terrain;

    // Resource acquisition
    void Awake()
    {
        terrain = GetComponent<Terrain>();
    }
	
    void OnCollisionStay(Collision other)
    {
        Wave waveObject = other.gameObject.GetComponent<Wave>();
        if(waveObject != null)
        {
            float pi2 = Mathf.PI * 2;
            float stepSize = 0.05f;
            
            float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);

            int waveFrames = Mathf.Max(waveObject.framesAlive, 8);

            float normalizedLife = Mathf.Clamp01(Mathf.Log(waveObject.life) + 1.1f) / waveObject.initialLife;

            for (int i = 0; i < waveFrames; i++)
            {
                float percentageOfFrames = ((float)i / (float)waveFrames);

                float totalRadius = waveObject.waveCollider.radius;
                float currentRadius = percentageOfFrames * totalRadius;

                float waveHeight = Mathf.Cos(percentageOfFrames * Mathf.PI*2.5f)/2;
                waveHeight *= normalizedLife;
                waveHeight += 0.5f;

                for (float theta = 0; theta < pi2; theta += stepSize)
                {
                    float x = currentRadius * Mathf.Cos(theta);
                    float z = currentRadius * Mathf.Sin(theta);

                    int[] coord = WorldspaceToHeightmapCoord(waveObject.transform.position + new Vector3(x,0,z));

                    heightmap[coord[1], coord[0]] = waveHeight;
                }
            }

            terrain.terrainData.SetHeightsDelayLOD(0,0,heightmap);
        }
    }

    public int[] WorldspaceToHeightmapCoord(Vector3 worldspaceCoord)
    {
        Vector3 tempCoord = (worldspaceCoord - transform.position);

        Vector3 coord;

        coord.x = tempCoord.x / terrain.terrainData.size.x;
        coord.y = tempCoord.y / terrain.terrainData.size.y;
        coord.z = tempCoord.z / terrain.terrainData.size.z;

        int[] finalCoord = { (int)(coord.x * terrain.terrainData.heightmapWidth), (int)(coord.z * terrain.terrainData.heightmapHeight) };

        return finalCoord;
        
    }
}
