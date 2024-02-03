using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
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
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
}
