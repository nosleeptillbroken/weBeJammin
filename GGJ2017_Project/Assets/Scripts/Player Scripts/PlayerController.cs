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
        IsGrounded();
        anim.SetFloat("Speed",player.GetAxis("Move"));
        anim.SetBool("isJumping",IsGrounded());
    }


    void JumpCounter()
    {
        if (isJumping)
        {
            jumpTime += (Time.deltaTime * 10);
            jumpTime = Mathf.Clamp(jumpTime, 0f, maxJumpTime);
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
        }
        if (player.GetButtonUp("Jump") && IsGrounded())
        {
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
            StartCoroutine(killMe());
            transform.GetComponent<Rigidbody>().freezeRotation = false;
        }

        // Hit a boulder?
        if (other.CompareTag("Boulder"))
        {
            Vector3 d = transform.position - other.transform.position;
            Vector3 v = other.GetComponent<Rigidbody>().velocity;

            if ((Vector3.Angle(d, v)) < 90)
            {
                StartCoroutine(killMe());
                transform.GetComponent<Rigidbody>().freezeRotation = false;
            }
        }

        if(other.CompareTag("Region"))
        {
            playerRegion = other.gameObject.GetComponent<Region>().ID;
        }
    }

    IEnumerator killMe()
    {
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