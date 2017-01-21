using UnityEngine;
using System.Collections;

public class Enemy_WallGuardian : MonoBehaviour
{
    public GameObject boulder;
    Vector3 boulderDir;
    public int hitForce;

    void Update()
    {
        boulderDir = Vector3.Normalize(boulder.transform.position - gameObject.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boulder") {
            Debug.Log("hit that ball");
            other.gameObject.GetComponent<Rigidbody>().AddForce(boulderDir * hitForce);
        }
    }
}
