using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class AIManager : MonoBehaviour
{
    public GameObject[] regionObjs;
    public List<Region> regionList;

    public GameObject[] nodeObjs;
    public List<Node> nodeList;


	void Awake ()
    {
        //Create list of Regions and Nodes for use by enemies.
        regionObjs = GameObject.FindGameObjectsWithTag("Region");
        for (int i = 0; i < regionObjs.Length; i++) {
            regionList.Add(regionObjs[i].GetComponent<Region>());
        }

        nodeObjs = GameObject.FindGameObjectsWithTag("Node");
        for (int i = 0; i < nodeObjs.Length; i++) {
            nodeList.Add(nodeObjs[i].GetComponent<Node>());
        }
    }
	
	
	void Update ()
    {
	
	}
}
