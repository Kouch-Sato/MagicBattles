using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDragonManager : MonoBehaviour
{
    public GameObject magicSprayPrefab;
    public GameObject head;
    GameObject player;
    Transform target;
    Animator animator;
    GameObject magicGameObject;
    float distance;
    bool isFlying;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        target = player.transform;
        this.transform.LookAt(target.position);
        distance = DirectionToPlayer().magnitude;
        Debug.Log(distance);

        if (magicGameObject)
        {
            magicGameObject.transform.position = head.transform.position;
            magicGameObject.transform.rotation = head.transform.rotation;
            magicGameObject.transform.Rotate(10, 0, 0);
        }

        if (distance > 25)
        {
            isFlying = true;
            animator.SetTrigger("TakeOff");
        }

        if (isFlying)
        {
            if (distance < 20)
            {
                animator.ResetTrigger("TakeOff");
                animator.SetTrigger("FlyFlame");
                isFlying = false;
            }
        }
    }

    public Vector3 DirectionToPlayer()
    {
        return new Vector3((target.position.x - transform.position.x), 0, (target.position.z - transform.position.z));
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
