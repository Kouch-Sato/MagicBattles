using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public int maxHP;
    int HP;

    public Collider weaponCollider;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        animator = GetComponent<Animator>();

        HideColliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
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

    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }

    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }
}
