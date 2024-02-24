using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float timeUntilNextEvent;

    // Start is called before the first frame update
    public override void Start()
    {
        float secondsPerShot;
        if(fireRate <= 0)
        {
            secondsPerShot = Mathf.Infinity;
        }
        else
        {
            secondsPerShot = 1 / fireRate;
        }
        timeUntilNextEvent = Time.time + secondsPerShot;

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Start();
        timeUntilNextEvent -= Time.deltaTime;
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        // Find vector to target
        Vector3 vectorToTarget = targetPosition - transform.position;
        // Find roatation to look at vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        // Rotate closer to that vector
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void RotateAway(Vector3 targetPosition)
    {
        // Find vector to target
        Vector3 vectorToTarget = targetPosition - transform.position;
        // Find roatation to look at vector
        Quaternion targetRotation = Quaternion.LookRotation(-vectorToTarget, Vector3.up);
        // Rotate closer to that vector
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {

        if (timeUntilNextEvent <= 0)
        {
            shooter.Shoot(shellPrefab, force, damage, shellLifespan);
            timeUntilNextEvent = fireRate;
        }
    }

    public override void MakeNoise()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = noiseMakerVolume;
        }
    }

    public override void StopNoise()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 0;
        }
    }
}
