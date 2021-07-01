using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMagic : MonoBehaviour
{
    public GameObject magicPrefab;
    public GameObject rainMagicPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            Attack();
            RainAttack();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RainAttack();
        }
    }

    private void Attack()
    {
        GameObject magicGameObject = Instantiate(magicPrefab, transform.position, transform.rotation) as GameObject;
        magicGameObject.GetComponent<Rigidbody>().AddForce(magicGameObject.transform.forward * 1000);
    }

    private void RainAttack()
    {
        Instantiate(rainMagicPrefab, transform.position, transform.rotation);
        Debug.Log(11111);
    }
}
