using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxHP;
    int HP;

    bool isDie = false;

    Animator animator;
    GameObject ingameSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        animator = GetComponent<Animator>();
        ingameSceneManager = GameObject.Find("IngameSceneManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("onTrigger");
        PlayerDamager damager = other.GetComponent<PlayerDamager>();
        if (damager)
        {
            GetDamage(damager.damage);
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("onExit");
        PlayerDamager damager = other.GetComponent<PlayerDamager>();
        if (damager || damager.onExitDamage)
        {
            GetDamage(damager.damage);
        }
    }

    public void GetDamage(int damage)
    {
        if ( isDie )
        {
            return;
        }

        animator.SetTrigger("GetHit");
        HP -= damage;

        if ( HP <= 0 )
        {
            HP = 0;
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        isDie = true;
        Destroy(gameObject, 2f);
        ingameSceneManager.GetComponent<IngameSceneManager>().enemyCount -= 1;
    }
}
