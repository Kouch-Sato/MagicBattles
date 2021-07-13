using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxHP;
    int HP;
    private float timeElapsedForTriggerStayAttack = 0;

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

    private void OnTriggerEnter(Collider other)
    {
        PlayerDamager damager = other.GetComponent<PlayerDamager>();
        if (damager)
        {
            GetDamage(damager.damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {    
        timeElapsedForTriggerStayAttack += Time.deltaTime;
        if (timeElapsedForTriggerStayAttack > 0.5f)
        {
            PlayerDamager damager = other.GetComponent<PlayerDamager>();
            if (damager || damager.onStayDamage)
            {
                GetDamage(damager.damage);
            }
            timeElapsedForTriggerStayAttack = 0;
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
        Destroy(gameObject, 5f);
        ingameSceneManager.GetComponent<IngameSceneManager>().enemyCount -= 1;
    }
}
