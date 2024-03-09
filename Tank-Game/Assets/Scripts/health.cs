using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float currenthealth;
    public float maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currenthealth -= amount;
        Debug.Log(source.name + "did " + "damage to " + gameObject.name);

        if(currenthealth <= 0)
        {
            Die(source); // value in method call here is an argument
        }
    }

    public void ReplenishHealth(float amount)
    {
        currenthealth += amount;
        currenthealth = Mathf.Clamp(currenthealth, 0, maxHealth);
    }

    public void Die(Pawn source) // input to a method
    {
        Debug.Log(source.name + " destroyed " + gameObject.name);
        Destroy(transform.parent.gameObject);
    }
}
