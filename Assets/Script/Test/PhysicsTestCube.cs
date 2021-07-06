using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTestCube : MonoBehaviour
{

    private float speed = 0.05f;
    private float elapsed = 0.0f;

    // 適当に動かしてぶつける
    void Update()
    {
        elapsed += Time.deltaTime;

        var pos = transform.position;
        transform.position = new Vector3(pos.x + speed, pos.y, pos.z);

        if (elapsed > 1.0f)
        {
            speed = -speed;
            elapsed = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.transform.name +  "/ CubeのOnCollisionEnter");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name + "/ CubeのOnTriggerEnter");
    }

}
