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
    
    void FixedUpdate ()
    {
        ProcessInput();
    }
    
    private void ProcessInput()
    {
        Vector3 MoveAxis = Vector3.forward + Vector3.right;
        Vector3 Move = ((transform.right * player.GetAxis("Strafe")) * moveSpeed) + ((transform.forward * player.GetAxis("Move")) * moveSpeed);
        Move.y = GetComponent<Rigidbody>().velocity.y;
        
        if (player.GetButtonDown("Jump") && IsGrounded()) //player jumps
        {
            Move.y = 10.0f;

            //
            GameObject newWaveObject = Instantiate(wavePrefab, transform.position, transform.rotation) as GameObject;
            Wave wave = newWaveObject.GetComponent<Wave>();
            //wave.initialLife = the lifespan you want (in seconds)
            //wave.life = wave.initialLife;
            //wave.rateOfExpansion = spread (in units per second)
            //
        }

        GetComponent<Rigidbody>().velocity = Move;//player moves left or right
        transform.Rotate(0f, player.GetAxis("Pan") * xSpeed * Time.deltaTime, 0f);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Terrain"));
    }
}