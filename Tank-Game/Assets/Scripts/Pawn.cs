using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // --[ Input ]--
    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;
    // hold Mover Component
    public Mover mover;
    // Variable to hold our Shooter
    public Shooter shooter;




    // --[ shooter data ]--
    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float force;
    // Variable for our damage done
    public float damage;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;

    // how fast to fire
    public float fireRate;

    // The noise tank produces
    public float noiseMakerVolume;
    public NoiseMaker noiseMaker;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        noiseMaker = GetComponent<NoiseMaker>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void RotateAway(Vector3 targetPosition);
    public abstract void Shoot();
    public abstract void MakeNoise();
    public abstract void StopNoise();
}
