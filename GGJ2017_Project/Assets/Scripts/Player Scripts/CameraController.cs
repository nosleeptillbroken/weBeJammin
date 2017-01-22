using UnityEngine;
using System.Collections;
using Rewired;

public class CameraController : MonoBehaviour
{
    private Player player; // The Rewired Player
    public int playerId = 0; // The Rewired player id of this character

    public Transform target;
    private float offset = 4f;
   
    private float ySpeed = 100f;
    private float yMinLimit = 3f;
    private float yMaxLimit = 30f;

    private float y = 0f;

    private Vector3 initialVector;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    // Use this for initialization
    void Start ()
	{
	    //Vector3 angles = transform.eulerAngles;
	    
	    //y = angles.y;

	    if (GetComponent<Rigidbody>() == true)
	    {
	        GetComponent<Rigidbody>().freezeRotation = true;
	    }
	}

    void Update()
    {
        if (Mathf.Abs(player.GetAxis("Tilt")) > 0.02f)
        {
            Vector3 currentAngles = transform.rotation.eulerAngles;

            if ((currentAngles.x < yMinLimit && player.GetAxis("Tilt") < 0) || (currentAngles.x > yMaxLimit && player.GetAxis("Tilt") > 0))
                return;

            transform.RotateAround(target.position + new Vector3(0f, offset, 0f), (transform.right),  player.GetAxis("Tilt") * ySpeed * Time.deltaTime);
        }
    }


    static float ClampAngle(float angle, float min, float max) //was used to clamp player tilt rotation
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
       return Mathf.Clamp(angle, min, max);
    }
}
