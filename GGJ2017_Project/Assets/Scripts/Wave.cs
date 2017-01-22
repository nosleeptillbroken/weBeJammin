using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class Wave : MonoBehaviour {

    // Max possible force applied
    public float maxForce = 10f;

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

    public float chromeMax = 10f;
    public float chromeCompression = 1f;
    public float chromeCompDecay = 0.05f;

    private Camera playerCamera;
    private VignetteAndChromaticAberration chromeAb;

    // Resource acquisition
    void Awake ()
    {
        waveCollider = GetComponent<SphereCollider>();
    }

	// Use this for initialization
	void Start ()
    {
        life = initialLife;
        playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
        chromeAb = playerCamera.GetComponent<VignetteAndChromaticAberration>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        waveCollider.radius += rateOfExpansion * Time.deltaTime;

        chromeAb.chromaticAberration = ((chromeMax - (framesAlive * chromeCompDecay)) * (rateOfExpansion / 40f)) 
            * Mathf.Sin(framesAlive * (chromeCompression + (framesAlive * chromeCompDecay)));

        life -= Time.deltaTime; framesAlive++;
        if (life <= 0.0f)
        {
            chromeAb.chromaticAberration = 0f;

            Destroy(this.gameObject);
        }
	}

    // This applies a force that decays based on the distance it is applied from
    private void OnTriggerStay(Collider other)
    {
        // Only apply to some objects
        if (other.CompareTag("Boulder") || other.CompareTag("Enemy"))
        {
            // Get normalized direction to apply force in
            Vector3 dir = (other.transform.position - transform.position);
            dir.Normalize();

            // Get the percentage of force to apply, 100% being at the same location
            float dist = Vector3.Distance(transform.position, other.transform.position);
            float linDecay = 1f / dist;

            // Apply this force to the other thing
            other.GetComponent<Rigidbody>().AddForce(dir * maxForce * linDecay);
        }

    }
}
