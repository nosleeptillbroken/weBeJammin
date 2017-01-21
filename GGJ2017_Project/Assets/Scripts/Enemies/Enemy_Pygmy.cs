using UnityEngine;
using System.Collections;

public class Enemy_Pygmy : MonoBehaviour
{
    //Internal References.
    Rigidbody rb;
    Transform tf;

    //External References.
    Transform player;
    Transform target;

    //
    Vector3 playerLocat;
    Vector3 dirVec;
    Vector3 toPlayer;
    Vector3 toTarget;
    Vector3 newDir;

    //Metrics.
    private float movSpeed = 2;
    private float rotSpeed = 1;
    private float movStep;
    private float rotStep;
	
	void Start ()
    {
        //Internal references set.
        rb = gameObject.GetComponent<Rigidbody>();
        tf = gameObject.GetComponent<Transform>();

        //External references set.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
	}
	
	
	void Update ()
    {
        movStep = Time.deltaTime * movSpeed;
        rotStep = Time.deltaTime * rotSpeed;

        playerLocat = player.position;

        //Vector calculations.
        toPlayer = playerLocat - tf.position;
        toTarget = toPlayer;

        dirVec = Vector3.Normalize(toTarget);
        newDir = Vector3.RotateTowards(tf.forward, dirVec, rotStep, 0.0f);

        tf.position = Vector3.MoveTowards(tf.position, playerLocat, movStep);
        tf.rotation = Quaternion.LookRotation(newDir);
        

        
        
        //Debug.Log(dirVec);
	}
}
