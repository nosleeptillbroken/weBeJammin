using UnityEngine;
using System.Collections;

public class SpikeTrapController : MonoBehaviour {
    private Animator anim;

    // Use this for initialization
    void Start() {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isActive", false);
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("isActive", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("isActive", false);
        }
    }
}
