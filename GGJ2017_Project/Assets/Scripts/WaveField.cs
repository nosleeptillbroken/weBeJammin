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
            float stepSize = 0.01f;

            float waveHeight = 1.0f * (waveObject.life / waveObject.initialLife);

            for (float theta = 0; theta < 2*Mathf.PI; theta += stepSize)
            {
                float x = waveObject.waveCollider.radius * Mathf.Cos(theta);
                float z = waveObject.waveCollider.radius * Mathf.Sin(theta);

                int[] terrainCoord = WorldspaceToHeightmapCoord(waveObject.transform.position + new Vector3(x,0,z));

                float[,] vals = { { waveHeight }, { waveHeight } };
                terrain.terrainData.SetHeightsDelayLOD(terrainCoord[0], terrainCoord[1], vals);
            }
                        
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
