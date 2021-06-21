using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public int maxHP;
    int hp;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        Debug.Log("HP:" + hp);

        if (hp < 0)
        {
            hp = 0;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
