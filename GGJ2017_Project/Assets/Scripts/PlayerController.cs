using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 3.0f;
    public float bulletSpeed = 15.0f;
    public GameObject bulletPrefab;

    private Player player; // The Rewired Player
    private PlayerController cc;
    private Vector3 moveVector;
    private bool fire;

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

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
        fire = player.GetButtonDown("Jump");
    }

    private void ProcessInput() {
        // Process movement
        if(moveVector.x != 0.0f || moveVector.y != 0.0f) {
            cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }

        // Process fire
        if(fire) {

        }
    }

    private void Move(Vector3 direction)
    {

    }
}