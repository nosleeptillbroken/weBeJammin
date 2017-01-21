using UnityEngine;
using System.Collections;

public class FireTrapController : MonoBehaviour {

    [SerializeField] private bool isActive = false;
    [SerializeField] private ParticleSystem fire;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("on");
            fire.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("off");
            fire.Stop();
        }
    }
}
