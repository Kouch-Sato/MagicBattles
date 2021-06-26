using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageManager : MonoBehaviour
{
    Transform target;
    public GameObject magicPrefab;

    public float timeOut = 5;
    private float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= timeOut) {
            Attack();

            timeElapsed = 0.0f;
        }
    }

    private void Attack()
    {
        Debug.Log(111);
        GameObject magicGameObject = Instantiate(magicPrefab, transform.position, transform.rotation) as GameObject;
        magicGameObject.transform.LookAt(target.position);
        magicGameObject.GetComponent<Rigidbody>().AddForce(magicGameObject.transform.forward * 1000);
  }
}
