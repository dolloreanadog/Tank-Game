using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDeltaExample : MonoBehaviour
{
    public float timerDelay = 1.0f;
    private float timeUntilNextEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Set the Delay
        timeUntilNextEvent = timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // remove time
        timeUntilNextEvent -= Time.deltaTime;

        // when timer reaches 0, activate
        if (timeUntilNextEvent <= 0)
        {
            Debug.Log("Itfs me!");
            timeUntilNextEvent = timerDelay;
        }
    }
}
