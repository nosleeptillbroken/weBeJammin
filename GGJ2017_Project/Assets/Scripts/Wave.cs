﻿using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

    // Initial life of the wave (in seconds)
    public float initialLife = 1.0f;

    // Life of the wave (in seconds)
    public float life;

    // Rate of expansion of the wave (in unity units per second)
    public float rateOfExpansion = 1.0f;

    // The sphere collider component
    public SphereCollider waveCollider;

    //
    public int framesAlive = 0;

    // Resource acquisition
    void Awake ()
    {
        waveCollider = GetComponent<SphereCollider>();
    }

	// Use this for initialization
	void Start ()
    {
        life = initialLife;
    }
	
	// Update is called once per frame
	void Update ()
    {
        waveCollider.radius += rateOfExpansion * Time.deltaTime;

        life -= Time.deltaTime; framesAlive++;
        if(life <= 0.0f) Destroy(this.gameObject);
	}
}
