using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberChair : MonoBehaviour
{

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = new Quaternion(0, 0, 0, 1);
    }


}
