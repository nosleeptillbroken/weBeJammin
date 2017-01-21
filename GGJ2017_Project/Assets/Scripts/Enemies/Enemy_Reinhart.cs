using UnityEngine;
using System.Collections;

public class Enemy_Reinhart : MonoBehaviour
{
    //Internal References.
    Rigidbody rb;
    Transform tf;

    //External References.
    Transform player;
    Transform target;

    //Vector Calculations.
    Vector3 playerLocat;
    Vector3 dirVec;
    Vector3 toPlayer;
    Vector3 newDir;

    //Region and Node Calculations.
    public int myRegion;
    public int playerRegion;

    //Metrics.
    private float movSpeed = 2;
    private float rotSpeed = 1;
    private float movStep;
    private float rotStep;

    void Start()
    {
        //Internal references set.
        rb = gameObject.GetComponent<Rigidbody>();
        tf = gameObject.GetComponent<Transform>();

        //External references set.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Calculate where to move to.
        playerRegion = 2;
    }


    void Update()
    {
        //Refreshed every frame.
        movStep = Time.deltaTime * movSpeed;
        rotStep = Time.deltaTime * rotSpeed;
        playerLocat = player.position;
        toPlayer = playerLocat - tf.position;

        //For now, player is always target.
        target = player;

        //Adjust directions to move and rotate.
        dirVec = Vector3.Normalize(toPlayer);
        newDir = Vector3.RotateTowards(tf.forward, dirVec, rotStep, 0.0f);

        //Position adjusted.
        if (myRegion == playerRegion)
        {
            tf.position = Vector3.MoveTowards(tf.position, target.position, movStep);
            tf.rotation = Quaternion.LookRotation(newDir);
        }

    }
}
