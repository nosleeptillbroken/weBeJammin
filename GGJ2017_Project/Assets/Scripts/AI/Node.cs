using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Node : MonoBehaviour
{
    public int ID;
    public int regID;
    public List<Node> connectedNodes;
    public Vector3 locat;

    void Awake()
    {
        locat = transform.position;
    }
}
