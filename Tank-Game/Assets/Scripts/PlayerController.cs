using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateCounterClockWiseKey;
    public KeyCode rotateClockWiseKey;
    public KeyCode fireCannon;


    // Start is called before the first frame update
    public override void Start()
    {
        // Check for game manager
        if(GameManager.instance != null)
        {
            GameManager.instance.players.Add(this);
        }
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process Keyboard Inputs
        ProcessInputs();

        //Run Update function from parent class
        base.Update();
    }

    public void ProcessInputs()
    {
        if(Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }   
        if (Input.GetKey(rotateCounterClockWiseKey))
        {
            pawn.RotateCounterClockwise();
        }
        if (Input.GetKey(rotateClockWiseKey))
        {
            pawn.RotateClockwise();
        }
        if (Input.GetKey(fireCannon))
        {
            pawn.Shoot();
        }
    }

    public void OnDestroy()
    {
        // check for game manager
        if(GameManager.instance != null)
        {
            // check for destroyed player
            if (GameManager.instance.players != null)
            {
                // Remove destroyed player
                GameManager.instance.players.Remove(this);
            }
        }
    }
}