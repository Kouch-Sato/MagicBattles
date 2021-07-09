using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMageManager : MonoBehaviour
{
    Transform target;
    Animator animator;
    public GameObject spherePrefab;
    public GameObject explosionPrefab;
    private float timeOutForBossAttack;
    private float timeElapsedForBossAttack = 0;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        timeOutForBossAttack = new System.Random().Next(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsedForBossAttack += Time.deltaTime;
        if(timeElapsedForBossAttack >= timeOutForBossAttack)
        {
            animator.SetTrigger("BossAttack");
            timeElapsedForBossAttack = 0.0f;
            timeOutForBossAttack = new System.Random().Next(5, 10);
        }
    }

    public void BossAtack()
    {
        // // mageのtransform.positionの調整
        Vector3 positionYAxisOffset = new Vector3(0.0f, 2.0f, 0.0f);

        float eachMagicInterval = 0.3f;
        int magicRadius = 6;

        var positionList = new List<Vector3>();
        positionList.Add(transform.position + transform.right * magicRadius);
        positionList.Add(transform.position + (transform.forward + transform.right).normalized * magicRadius);
        positionList.Add(transform.position + transform.forward * magicRadius);
        positionList.Add(transform.position + (transform.forward - transform.right).normalized * magicRadius);
        positionList.Add(transform.position - transform.right * magicRadius);

        for (int i=0; i < positionList.Count; i++)
        {
            // Enemyに近い方から発火
            StartCoroutine (MagicInstatiateAndDestroy(positionList[i] + positionYAxisOffset, eachMagicInterval * i));
        }

        IEnumerator MagicInstatiateAndDestroy(Vector3 position, float interval) {
            yield return new WaitForSeconds (interval);
            GameObject sphereObject = Instantiate(spherePrefab, position, transform.rotation) as GameObject;
            GameObject explosionObject = Instantiate(explosionPrefab, position, transform.rotation) as GameObject;
            Destroy(explosionObject, 1.0f);

            yield return new WaitForSeconds (4);
            sphereObject.transform.LookAt(target.position);
            sphereObject.GetComponent<Rigidbody>().AddForce(sphereObject.transform.forward * 2000);
            Destroy(sphereObject, 1.4f);
        }
    }
}
