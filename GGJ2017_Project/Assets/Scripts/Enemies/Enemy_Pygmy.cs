using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemy_Pygmy : MonoBehaviour
{
    //Internal References.
    Rigidbody rb;
    Transform tf;

    //External References.
    Transform player;
    Transform target;
    public Transform home;
    public bool isHome;

    //Vector Calculations.
    Vector3 playerLocat;
    Vector3 dirVec;
    Vector3 toPlayer;
    Vector3 toHome;
    Vector3 toTarget;
    Vector3 newDir;

    //Region and Node Calculations.
    public int myRegion;
    public int playerRegion;

    //Metrics.
    private float movSpeed = 4;
    private float rotSpeed = 2;
    private float movStep;
    private float rotStep;
	
	void Start ()
    {
        //Internal references set.
        rb = gameObject.GetComponent<Rigidbody>();
        tf = gameObject.GetComponent<Transform>();

        //External references set.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Calculate where to move to.
        playerRegion = 2;
    }
	
	
	void Update ()
    {
        //Refreshed every frame.
        movStep = Time.deltaTime * movSpeed;
        rotStep = Time.deltaTime * rotSpeed;
        playerLocat = player.position;
        toPlayer = playerLocat - tf.position;
        toHome = home.position - tf.position;

        //If the player is present, move to the player.
        if (playerRegion == myRegion) {
            toTarget = toPlayer;
            target = player;
        }
        else {
            toTarget = toHome;
            target = home;
        }

        //Adjust directions to move and rotate.
        dirVec = Vector3.Normalize(toTarget);
        newDir = Vector3.RotateTowards(tf.forward, dirVec, rotStep, 0.0f);

        //Position adjusted.
        if (myRegion == playerRegion || isHome == false) {
            tf.position = Vector3.MoveTowards(tf.position, target.position, movStep);
            tf.rotation = Quaternion.LookRotation(newDir);
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PygmyHome")
            isHome = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PygmyHome")
            isHome = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boulder")
            Destroy(gameObject);
    }

}




