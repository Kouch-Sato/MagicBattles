using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 1000;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        Damager damager = other.GetComponent<Damager>();
        if (damager)
        {
            GetDamage(damager.damage);
        }
    }

     void GetDamage(int damage)
     {
         hp -= damage;
     }
}
