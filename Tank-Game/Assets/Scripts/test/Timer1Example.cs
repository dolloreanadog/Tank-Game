using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer1Example : MonoBehaviour
{
    public float timerDelay = 1.0f;
    private float nextEventTime;


    // Start is called before the first frame update
    void Start()
    {
        // Set the Delay
        nextEventTime = Time.time + timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // When Time hits delay activate
        if (Time.time >= nextEventTime)
        {
            Debug.Log("ItÅfs me!");
            nextEventTime = Time.time + timerDelay;
        }
    }
}
