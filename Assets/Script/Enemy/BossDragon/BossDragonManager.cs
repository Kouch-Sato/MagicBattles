using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragonManager : MonoBehaviour
{
    public GameObject magicSprayPrefab;
    public GameObject head;
    Transform target;
    Animator animator;
    GameObject magicGameObject;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        this.transform.LookAt(target.position);

        if (magicGameObject)
        {
            magicGameObject.transform.position = head.transform.position;
            // magicGameObject.transform.LookAt(target.position);
            magicGameObject.transform.rotation = head.transform.rotation;
            magicGameObject.transform.Rotate(10, 0, 0);
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
