using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TankMover : Mover
{

    private Rigidbody rb;
    private Transform tf;

    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }
    public override void Rotate(float turnSpeed)
    {

        tf.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
