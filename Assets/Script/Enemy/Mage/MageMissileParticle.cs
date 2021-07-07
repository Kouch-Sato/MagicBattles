using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMissileParticle : MonoBehaviour
{
    public GameObject explosionPrefab;
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" || other.tag == "EnemyStaff")
        {
            return;
        }

        GameObject explosionObject = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(explosionObject, 1.0f);
    }
}
