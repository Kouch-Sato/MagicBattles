using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxHP;
    int HP;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDamage(int damage)
    {
        animator.SetTrigger("GetHit");
        HP -= damage;

        if (HP < 0)
        {
            HP = 0;
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}
