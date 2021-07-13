using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileParticle : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" || other.tag == "PlayerStaff")
        {
            return;
        }

        GameObject explosionObject = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(explosionObject, 1.0f);
    }
}
