using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTestSphere : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.transform.name + "/ SphereのOnCollisionEnter");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name + "/ SphereのOnTriggerEnter");
    }
}
