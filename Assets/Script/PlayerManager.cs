using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 1000;
    int HP;
    public PlayerUIManager playerUIManager;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        playerUIManager.Init(this);
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
        HP -= damage;
        Debug.Log(HP);
        playerUIManager.GetDamage(HP);
    }
}
