using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 0.25f;

    private Player player; // The Rewired Player
    private PlayerController cc;
    private Vector3 moveVector;
    private bool jump;

    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<PlayerController>();
    }

    void Update () {
        GetInput();
        ProcessInput();
    }

    private void GetInput() {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.


        jump = player.GetButtonDown("Jump");
    }

    private void ProcessInput() {
        Vector3 Move = new Vector3(player.GetAxis("Strafe"), 0f, player.GetAxis("Move"));
        GetComponent<Rigidbody>().velocity = Move * moveSpeed;
        if (jump)
        {
            Debug.Log("Jumping");
        }
    }
}