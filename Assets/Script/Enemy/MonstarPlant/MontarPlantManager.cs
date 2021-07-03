using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontarPlantManager : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
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
            Attack();

            timeElapsed = 0.0f;
        }
    }

    public void Attack()
    {
        Vector3 center = (transform.position + target.position) * 0.5f;
        GameObject magicGameObject = Instantiate(magicPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(magicGameObject, 1.4f);
    }
}
