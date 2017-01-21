using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour {

    public int playerId = 0; // The Rewired player id of this character
    private float xSpeed = 150f;
    public float moveSpeed = 0.50f;

    private Player player; // The Rewired Player

    public GameObject wavePrefab;

    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }


    void Update ()
    {
        ProcessInput();
    }



    private void ProcessInput() {
       Vector3 Move = ((transform.right * player.GetAxis("Strafe")) * moveSpeed) + ((transform.forward * player.GetAxis("Move")) * moveSpeed);

        GetComponent<Rigidbody>().velocity = Move; //player moves left or right
        transform.Rotate(0f, player.GetAxis("Pan") * xSpeed * Time.deltaTime, 0f);

        if (player.GetButtonDown("Jump")) //player jumps
        {
            //Instantiate(wavePrefab, transform.position, transform.rotation);
        }
    }
}