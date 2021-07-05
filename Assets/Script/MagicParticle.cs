using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicParticle : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int damage;

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
        Debug.Log(other.name);
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        if (other.tag == "Enemy")
        {
            Debug.Log("aaaaaa");
            other.GetComponent<EnemyManager>().GetDamage(damage);
        }
    }
}
