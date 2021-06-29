using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageManager : MonoBehaviour
{
    Transform target;
    public Transform weapon;
    Animator animator;
    public GameObject magicPrefab;

    public float timeOut = 5;
    private float timeElapsed = 0;

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
        }
    }

    public void Attack()
    {
        GameObject magicGameObject = Instantiate(magicPrefab, weapon.position, weapon.rotation) as GameObject;
        magicGameObject.transform.LookAt(target.position);
        magicGameObject.GetComponent<Rigidbody>().AddForce(magicGameObject.transform.forward * 2000);
    }
}
