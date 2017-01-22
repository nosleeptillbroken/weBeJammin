using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Goal : MonoBehaviour {

    public UnityEvent onBoulderEnter;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boulder"))
        {
            onBoulderEnter.Invoke();
        }
    }
	
}
