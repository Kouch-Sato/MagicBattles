using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageManager : MonoBehaviour
{
    Transform target;
    public Transform weaponTransform;
    public GameObject weaponStaff;
    public Vector3 weaponSize;
    Animator animator;
    public GameObject magicPrefab;

    public float timeOut = 5;
    private float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        weaponSize = weaponStaff.GetComponent<SkinnedMeshRenderer>().bounds.size;
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
        // 魔法の発射位置が、ステッキの上部に来るように調整
        var weaponOffset = new Vector3(0.0f, weaponSize.y / 3.0f, 0.0f);
        
        GameObject magicGameObject = Instantiate(magicPrefab, weaponTransform.position + weaponOffset, weaponTransform.rotation) as GameObject;
        magicGameObject.transform.LookAt(target.position);
        magicGameObject.GetComponent<Rigidbody>().AddForce(magicGameObject.transform.forward * 2000);
    }
}
