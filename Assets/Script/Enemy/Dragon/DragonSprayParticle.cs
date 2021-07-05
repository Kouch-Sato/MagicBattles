using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSprayParticle : MonoBehaviour
{
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
        if (other.tag == "Enemy")
        {
            return;
        }

        Debug.Log(other.name);

        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().GetDamage(damage);
        }
    }
}
