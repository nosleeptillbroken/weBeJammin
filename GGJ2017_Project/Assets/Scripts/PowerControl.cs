using UnityEngine;
using System.Collections;

public class PowerControl : MonoBehaviour
{
    public int power = 0;
    int maxPower = 100;
    
    private int increase()
    {
        power += 1;
        return power;
    }

    private int decrease()
    {
        power -= 1;
        return power;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButton("Fire") && power <= maxPower)
        {
            increase();
        }
        else
        {
            decrease();
        }

	}
}
