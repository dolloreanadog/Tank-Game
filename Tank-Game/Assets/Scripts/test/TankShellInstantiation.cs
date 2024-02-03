using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShellInstantiation : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = Instantiate(prefab, transform.position, transform.rotation) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }
}