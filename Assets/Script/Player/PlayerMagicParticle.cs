using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicParticle : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            return;
        }

        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyManager>().GetDamage(damage);
        }
    }
}