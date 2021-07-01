using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMagic : MonoBehaviour
{
    public GameObject magicPrefab;
    public GameObject rainMagicPrefab;
    public GameObject OVRPlayerController;

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
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RainAttack();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
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
        Instantiate(rainMagicPrefab, OVRPlayerController.transform.position + new Vector3(0, -1.0f, 0), Quaternion.Euler(-90, 0, 0));
        // Instantiate(rainMagicPrefab);
        Debug.Log(11111);
    }
}
