using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private int goal = 2;
    public int goalCount;
    public GameObject boulderPrefab;
    // Use this for initialization
    void Start () {
	
	}

    void WinCheck()
    {
        if (goalCount == goal)
        {
            SceneManager.LoadScene("Victory_Scene");
        }
    }

    public void IncreaseScore()
    {
        goalCount += 1;
        boulderPrefab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        DestroyObject(boulderPrefab, 3f);
    }
	
	// Update is called once per frame
	void Update () {
	    WinCheck();
	}
}
