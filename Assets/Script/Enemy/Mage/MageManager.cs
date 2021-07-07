using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageManager : MonoBehaviour
{
    Transform target;
    public Transform weaponTransform;
    public GameObject weaponStaff;
    public Vector3 weaponSize;
    Animator animator;
    public GameObject magicPrefab;
    public GameObject shieldPrefab;
    NavMeshAgent agent;

    private float timeOutForAttack;
    private float timeOutForMove;
    private float timeOutForShield;
    private float timeElapsedForAttack = 0;
    private float timeElapsedForMove = 0;
    private float timeElapsedForShield = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        timeOutForAttack = new System.Random().Next(6, 12);
        timeOutForMove = new System.Random().Next(2, 5);
        timeOutForShield = new System.Random().Next(7, 11);
        weaponSize = weaponStaff.GetComponent<SkinnedMeshRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.position);

        timeElapsedForAttack += Time.deltaTime;
        timeElapsedForMove += Time.deltaTime;
        timeElapsedForShield += Time.deltaTime;

        if(timeElapsedForAttack >= timeOutForAttack) {
            animator.SetTrigger("Attack");
            timeElapsedForAttack = 0.0f;
            timeOutForAttack = new System.Random().Next(6, 12);
        }

        if(timeElapsedForMove >= timeOutForMove)
        {
            RandomMove();
            timeElapsedForMove = 0.0f;
            timeOutForMove = new System.Random().Next(2, 5);
        }

        if(timeElapsedForShield >= timeOutForShield)
        {
            animator.SetTrigger("Shield");
            timeElapsedForShield = 0.0f;
            timeOutForShield = new System.Random().Next(8, 14);
        }
    }

    public void Attack()
    {
        // 魔法の発射位置が、ステッキの上部に来るように調整
        var weaponOffset = new Vector3(0.0f, weaponSize.y / 3.0f, 0.0f);
        
        GameObject magicGameObject = Instantiate(magicPrefab, weaponTransform.position + weaponOffset, weaponTransform.rotation) as GameObject;
        magicGameObject.transform.LookAt(target.position);
        magicGameObject.GetComponent<Rigidbody>().AddForce(magicGameObject.transform.forward * 2000);
    }

    public void Shield()
    {
        GameObject shieldObject = Instantiate(shieldPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(shieldObject, 6.0f);
    }

    private void RandomMove()
    {
        string[] directions = new string[] { "forward", "backward", "right", "left" };
        string direction = directions[new System.Random().Next(0, directions.Length)];
        
        switch (direction)
        {
            // transform.forward(right)等は、現在のgameobjectの向きベクトルに対して、指定した方向を示す大きさ1のVector3
            case "forward":
                animator.SetTrigger("WalkForward");
                agent.destination = transform.position + transform.forward;
                break;
            case "backward":
                animator.SetTrigger("WalkBackward");
                agent.destination = transform.position - transform.forward;
                break;
            case "right":
                animator.SetTrigger("WalkRight");
                agent.destination = transform.position + transform.right;
                break;
            case "left":
                animator.SetTrigger("WalkLeft");
                agent.destination = transform.position - transform.right;
                break;
        }

    }
}
