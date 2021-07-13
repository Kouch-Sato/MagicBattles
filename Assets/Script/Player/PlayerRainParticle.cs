using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRainParticle : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            Vector3 positionYAxisOffset = new Vector3(0, 1.0f, 0);
            GameObject explosionObject = Instantiate(explosionPrefab, other.transform.position  + positionYAxisOffset, other.transform.rotation) as GameObject;
        　　Destroy(explosionObject, 1.0f);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            Vector3 positionYAxisOffset = new Vector3(0, 1.0f, 0);
            GameObject explosionObject = Instantiate(explosionPrefab, other.transform.position  + positionYAxisOffset, other.transform.rotation) as GameObject;
        　　Destroy(explosionObject, 1.0f);
        }
    }
}
