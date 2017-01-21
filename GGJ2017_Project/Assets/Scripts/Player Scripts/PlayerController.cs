using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 0.25f;

    private Player player; // The Rewired Player
    private bool jump;

    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update () {
        ProcessInput();
    }

   

    private void ProcessInput() {
        Vector3 Move = new Vector3(player.GetAxis("Strafe"), 0f, player.GetAxis("Move"));
        GetComponent<Rigidbody>().velocity = Move * moveSpeed; //player moves left or right


        if (player.GetButtonDown("Jump")) //player jumps
        {
            Debug.Log("Jumping");
        }
    }

    private void Move(Vector3 direction)
    {

    }
}