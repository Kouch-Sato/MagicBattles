using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlimeManager : MonoBehaviour
{
    Transform target;

    Animator animator;
    public GameObject magicPrefab;

    public float timeOut = 5;
    private float timeElapsed = 0;
    private float timeRandomOffset;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.position);

        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeOut) {
            animator.SetTrigger("Attack");

            timeElapsed = 0.0f;
            // 次回の攻撃までの感覚を不規則に
            timeOut = new System.Random().Next(6, 12);
        }
    }

    void Attack()
    {
        GameObject magicGameObject = Instantiate(magicPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(magicGameObject, 0.5f);
    }
}
