using UnityEngine;
using System.Collections;

public class SnakeTrapController : MonoBehaviour {

    [SerializeField] private Transform snakeSpawn;
    [SerializeField] private GameObject snake;

    public bool isActive = false;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (isActive)
        {
            InvokeRepeating("SpawnSnakes", 1.0f, 4.0f);
        }
        else
            CancelInvoke();
    }

    void SpawnSnakes()
    {
        Instantiate(snake, snakeSpawn.position, snakeSpawn.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isActive = true;
            Debug.Log("snaketrap active");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = false;
            Debug.Log("snaketrap inactive");
        }
    }
}
