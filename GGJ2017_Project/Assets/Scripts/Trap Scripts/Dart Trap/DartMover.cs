using UnityEngine;
using System.Collections;

public class DartMover : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 1, 0);
	}

    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
