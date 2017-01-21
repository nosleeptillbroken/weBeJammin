using UnityEngine;
using System.Collections;
using Rewired;

public class CameraController : MonoBehaviour
{
    private Player player; // The Rewired Player
    public int playerId = 0; // The Rewired player id of this character

    public Transform target;
    private float distance = 5f;
   
    private float ySpeed = 25f;
    private float yMinLimit = 3f;
    private float yMaxLimit = 30f;

    private float y = 0f;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    // Use this for initialization
    void Start ()
	{
	    Vector3 angles = transform.eulerAngles;
	    
	    y = angles.y;

	    if (GetComponent<Rigidbody>() == true)
	    {
	        GetComponent<Rigidbody>().freezeRotation = true;
	    }
	}

    void Update()
    {
        if (Mathf.Abs(player.GetAxis("Tilt")) > 0.02f)
        {
            transform.RotateAround(target.position, transform.right,  player.GetAxis("Tilt") * ySpeed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
            
            y += player.GetAxis("Tilt") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, 0, 0);
            Vector3 position = rotation * new Vector3(0f, 0f, -distance) + target.position;


            //transform.rotation = rotation;
            //transform.position = position;
            
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
       return Mathf.Clamp(angle, min, max);
    }
}
