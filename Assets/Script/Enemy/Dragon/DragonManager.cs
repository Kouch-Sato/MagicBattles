using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonManager : MonoBehaviour
{
    public GameObject magicPrefab;
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    GameObject magicGameObject;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        target = GameObject.FindWithTag("Player").transform;
        this.transform.LookAt(target.position);
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    public void StartAttack()
    {
        magicGameObject = Instantiate(magicPrefab, transform.position + new Vector3(0, 1.0f, 0), transform.rotation) as GameObject;
        magicGameObject.transform.LookAt(target.position + new Vector3(0, -1.0f, 0));
    }

    public void StopAttack()
    {
        Destroy(magicGameObject);
    }
}
