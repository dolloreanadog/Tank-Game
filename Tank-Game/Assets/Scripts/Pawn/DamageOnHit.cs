using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        // Get health component from colliding game object
        health otherHealth = other.gameObject.GetComponent<health>();
        Debug.Log(otherHealth);
        if (otherHealth != null)
        {
            //do damage
            Debug.Log("Hit");
            otherHealth.TakeDamage(damageDone, owner);

        }

        // Destroy Object
        Destroy(gameObject);
    }
}
