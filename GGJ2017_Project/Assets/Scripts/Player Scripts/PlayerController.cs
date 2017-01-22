using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Rewired;

public class PlayerController : MonoBehaviour
{

    public int playerId = 0; // The Rewired player id of this character
    public int playerRegion;
    private float xSpeed = 150f;
    public float moveSpeed = 0.50f;
    private float jumpTime;
    private float maxJumpTime = 40f;
    private bool isJumping = false;
    private Player player; // The Rewired Player

    public GameObject wavePrefab;

    private Animator anim;

    [SerializeField]
    private float deathWait = 5f;

    private float minVibeAmt = 0.25f;
    private float maxVibeAmt = 0.66f;
    private float ultWaveVibe = 0.8f;


    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        ProcessInput();
        JumpCounter();
    }

    void Update()
    {
        //IsGrounded();
        anim.SetFloat("Speed",player.GetAxis("Move"));
        anim.SetBool("isGrounded",IsGrounded());
    }

    public void StopVibration()
    {
        // Set vibration for a certain duration
        foreach (Joystick j in player.controllers.Joysticks)
        {
            if (!j.supportsVibration) continue;
            if (j.vibrationMotorCount > 0) j.SetVibration(0f, 0f); // 1 second duration
        }
    }

    void JumpCounter()
    {

        if (isJumping)
        {
            jumpTime += (Time.deltaTime * 15);
            jumpTime = Mathf.Clamp(jumpTime, 0f, maxJumpTime);

            if (jumpTime < 40f)
            {
                anim.SetBool("isJumping", true);
                float vibAmt = Mathf.PingPong(minVibeAmt, maxVibeAmt);
                // Set vibration for a certain duration
                foreach (Joystick j in player.controllers.Joysticks)
                {
                    if (!j.supportsVibration) continue;
                    if (j.vibrationMotorCount > 0) j.SetVibration(vibAmt, vibAmt); // ping pong between two values controller via vibAmt
                }
            }
            else
            {
                anim.SetBool("isCharging", true);
                // Set vibration for a certain duration
                foreach (Joystick j in player.controllers.Joysticks)
                {
                    if (!j.supportsVibration) continue;
                    if (j.vibrationMotorCount > 0) j.SetVibration(ultWaveVibe, ultWaveVibe); // go all out when you hit wave cap
                }
            }

            
        }
    }

    private void ProcessInput()
    {
        
        Vector3 MoveAxis = Vector3.forward + Vector3.right;
        Vector3 Move = ((transform.right * player.GetAxis("Strafe")) * moveSpeed) + ((transform.forward * player.GetAxis("Move")) * moveSpeed);
        Move.y = GetComponent<Rigidbody>().velocity.y;


        if (player.GetButtonDown("Jump") && IsGrounded()) //player jumps
        {
            isJumping = true;
            //anim.SetBool("isJumping", true);
        }
        if (player.GetButtonUp("Jump") && IsGrounded())
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isCharging", false);
            StopVibration();
            isJumping = false;
            Move.y = 10.0f;
            GameObject newWaveObject = Instantiate(wavePrefab, (transform.position - Vector3.up), transform.rotation) as GameObject;
            Wave wave = newWaveObject.GetComponent<Wave>();
            
            
            if (jumpTime <= 1.2f)
            {
                wave.rateOfExpansion = 5f;
                wave.initialLife /= 1.25f;
                wave.life = wave.initialLife;
            }
            else
            {
            wave.rateOfExpansion = jumpTime;
            }
            jumpTime = 0;
        }

        GetComponent<Rigidbody>().velocity = Move;//player moves left or right
        transform.Rotate(0f, player.GetAxis("Pan") * xSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Hit an enemy?
        if (other.CompareTag("Enemy"))
        { 
            StartCoroutine(KillMe());
        }

        // Hit a boulder?
        if (other.CompareTag("Boulder"))
        {
            Vector3 d = transform.position - other.transform.position;
            Vector3 v = other.GetComponent<Rigidbody>().velocity;

            if ((Vector3.Angle(d, v)) < 90)
            {
                StartCoroutine(KillMe());
            }
        }

        if(other.CompareTag("Region"))
        {
            playerRegion = other.gameObject.GetComponent<Region>().ID;
        }
    }

    IEnumerator KillMe()
    {
        transform.GetComponent<Rigidbody>().freezeRotation = false;
        yield return new WaitForSeconds(deathWait);
        SceneManager.LoadScene("Game_Over");
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Region"))
        {
            playerRegion = 0;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Terrain"));
    }
}