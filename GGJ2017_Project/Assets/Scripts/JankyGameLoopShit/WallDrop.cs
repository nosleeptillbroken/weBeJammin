using UnityEngine;
using System.Collections;

public class WallDrop : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject boulderPrefab;
	// Use this for initialization
	void Start () {
        
	}

    public void DropWall()
    {
        boulderPrefab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        wallPrefab.GetComponentInChildren<BoxCollider>().isTrigger = true;
        DestroyObject(wallPrefab, 3f);
        GameObject.Find("GameController").GetComponent<GameController>().goalCount += 1;
        DestroyObject(boulderPrefab,3f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
