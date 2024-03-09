using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Shoot(GameObject shellPrefab, float force, float damage, float lifespan)
    {
        // Instantiate tank shell
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        // Get DamageOnHit Componenent
        DamageOnHit damageOnHit = newShell.GetComponent<DamageOnHit>();

        // if DamageOnHit exists, do damage
        if (damageOnHit != null)
        {
            damageOnHit.damageDone = damage;

            damageOnHit.owner = GetComponent<Pawn>();

        }

        // Get the rigidbody component
        Rigidbody rb = newShell.GetComponent<Rigidbody>();


        // if rigidbody exists, add force
        if (rb != null)
        {
            //Debug.Log(firepointTransform.forward * force);
            rb.AddForce(firepointTransform.forward * force);
        }

        Destroy(newShell, lifespan);
    }

}
