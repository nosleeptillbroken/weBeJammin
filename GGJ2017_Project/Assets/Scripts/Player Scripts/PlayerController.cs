using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character
    private float xSpeed = 30f;
    public float moveSpeed = 0.25f;

    private float x = 0f;

    private Player player; // The Rewired Player
    private bool jump;

    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
    }

    void Update () {
        ProcessInput();
    }

    void LateUpdate()
    {
        //x += player.GetAxis("Pan") * xSpeed * 0.02f;
        
    }

    private void ProcessInput() {
       Vector3 Move = ((transform.right * player.GetAxis("Strafe")) * moveSpeed) + ((transform.forward * player.GetAxis("Move")) * moveSpeed);

        GetComponent<Rigidbody>().velocity = Move; //player moves left or right
        transform.Rotate(0f, player.GetAxis("Pan") * xSpeed * Time.deltaTime, 0f);

        if (player.GetButtonDown("Jump")) //player jumps
        {
            Debug.Log("Jumping");
        }
    }

    private void Move(Vector3 direction)
    {

    }
}