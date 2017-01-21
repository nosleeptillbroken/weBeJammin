using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 5.0f;
    public float runSpeed = 150.0f;
    public float turnSpeed = 5.0f;

    //
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.0f + 0.1f);
    }

    //
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * turnSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            GetComponent<Rigidbody>().velocity += new Vector3(0, jumpHeight, 0);
        }
    }
}
