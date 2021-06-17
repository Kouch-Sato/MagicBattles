using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicParticle : MonoBehaviour
{
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(11);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
        Debug.Log(1234);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
