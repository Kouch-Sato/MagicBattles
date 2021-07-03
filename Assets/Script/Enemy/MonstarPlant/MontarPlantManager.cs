using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

            // 次回の攻撃までの感覚を不規則に
            timeOut = new System.Random().Next(6, 12);
        }
    }

    public void Attack()
    {
        // 地表に位置を合わせるための調整
        Vector3 positionYAxisOffset = new Vector3(0.0f, 0.7f, 0.0f);

        // Enemyに近い方から発火
        Vector3 positionFirst = (transform.position * 2.0f + target.position * 1.0f) / 3.0f - positionYAxisOffset;
        Vector3 positionSecond = (transform.position * 1.0f + target.position * 2.0f) / 3.0f - positionYAxisOffset;
        Vector3 positionThird = target.position + (transform.position - target.position).normalized * 0.8f - positionYAxisOffset;

        Quaternion magicQuaternion = Quaternion.Euler(-90, 0, 0);

        float firstMagicInterval = 0.6f;
        float eachMagicInterval = 0.3f;

        StartCoroutine (MagicInstatiateAndDestroy(positionFirst, firstMagicInterval + eachMagicInterval * 0.0f));
        StartCoroutine (MagicInstatiateAndDestroy(positionSecond, firstMagicInterval + eachMagicInterval * 1.0f));
        StartCoroutine (MagicInstatiateAndDestroy(positionThird, firstMagicInterval + eachMagicInterval * 2.0f));

        IEnumerator MagicInstatiateAndDestroy(Vector3 position, float interval) {
            yield return new WaitForSeconds (interval);
            GameObject magicGameObject = Instantiate(magicPrefab, position, magicQuaternion) as GameObject;
            Destroy(magicGameObject, 1.4f);
        }
    }
}
