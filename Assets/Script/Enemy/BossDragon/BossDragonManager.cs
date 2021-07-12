using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDragonManager : MonoBehaviour
{
    public GameObject magicSprayPrefab;
    public GameObject head;
    Transform target;
    Animator animator;
    GameObject magicGameObject;
    float distance;
    bool isFlying;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        agent.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        this.transform.LookAt(target.position);
        distance = Vector3.Distance(target.position, transform.position);

        if (magicGameObject)
        {
            magicGameObject.transform.position = head.transform.position;
            magicGameObject.transform.rotation = head.transform.rotation;
            magicGameObject.transform.Rotate(10, 0, 0);
        }

        if (distance > 20)
        {
            isFlying = true;
            animator.SetTrigger("TakeOff");
        }

        if (isFlying)
        {
            if (distance < 10)
            {
                agent.speed = 0;
                animator.ResetTrigger("TakeOff");
                animator.SetTrigger("FlyFlame");
                isFlying = false;
            }

            agent.speed = 5;
        }
    }

    public void StartAttack()
    {
        magicGameObject = Instantiate(magicSprayPrefab, head.transform.position, transform.rotation) as GameObject;
        magicGameObject.transform.LookAt(target.position);
    }

    public void StopAttack()
    {
        Destroy(magicGameObject);
    }
}
