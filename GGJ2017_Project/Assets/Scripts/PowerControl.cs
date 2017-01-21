using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerControl : MonoBehaviour
{
    public int startingPower = 0;
    public int currentPower;
    public int maxPower = 100;
    public Slider PowerSlider;
	
    void Awake()
    {
        currentPower = startingPower;
    }

	// Update is called once per frame
	void Update ()
    {
        while (Input.GetKey(KeyCode.N))
        {
            currentPower = currentPower + 1;
        }

    }
}
