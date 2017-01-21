using UnityEngine;
using System.Collections;

public class DartTrapController : MonoBehaviour {

    [SerializeField] private Transform dartSpawner;
    [SerializeField] private GameObject dart;
    [SerializeField] private bool isActive = false;
    public float fireRate = 3.0f;
    private float fireCtr;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update()
    {

        if (isActive && (fireCtr >= fireRate))
        {
            fireCtr = 0;
            Spawn();
        }
        else
        {
            fireCtr += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isActive = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
        }
    }

    void Spawn()
    {
        Instantiate(dart, dartSpawner.position, dartSpawner.rotation);
    }
}
