using UnityEngine;
using System.Collections;

public class EntityKiller : MonoBehaviour {

    public bool killPlayers = true;
    public bool killEnemies = true;

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && killPlayers)
        {
            other.gameObject.GetComponent<PlayerController>().StartCoroutine("KillMe");
        }
        else if (other.gameObject.CompareTag("Enemy") && killEnemies)
        {
            Destroy(other.gameObject);
        }
    }
    
}
