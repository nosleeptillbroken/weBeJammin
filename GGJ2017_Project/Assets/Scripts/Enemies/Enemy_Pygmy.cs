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
    PlayerController playerScript;
    Transform target;
    public Transform[] home;
    public int homeIndex;
    private Animator runAnim;

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
    public float movSpeed;
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
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        runAnim = GetComponentInChildren<Animator>();
    }
	
	
	void Update ()
    {
        //Refreshed every frame.
        movStep = Time.deltaTime * movSpeed;
        rotStep = Time.deltaTime * rotSpeed;
        playerLocat = player.position;
        toPlayer = playerLocat - tf.position;
        toHome = home[homeIndex].position - tf.position;
        playerRegion = playerScript.playerRegion;

        //If the player is present, move to the player.
        if (playerRegion == myRegion) {
            toTarget = toPlayer;
            target = player;
        }
        else {
            toTarget = toHome;
            target = home[homeIndex];
        }

        //Adjust directions to move and rotate.
        dirVec = Vector3.Normalize(toTarget);
        newDir = Vector3.RotateTowards(tf.forward, dirVec, rotStep, 0.0f);

        //Adjust position.
        tf.position = Vector3.MoveTowards(tf.position, target.position, movStep);
        tf.rotation = Quaternion.LookRotation(newDir);

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PygmyHome") {
            if (homeIndex < 5)
                homeIndex++;
            else
                homeIndex = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boulder")
            Destroy(gameObject);
    }

}




