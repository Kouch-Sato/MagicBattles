using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 1000;
    public int HP;
    public float maxMP = 100.0f;
    float MP;
    public bool isDie;
    public PlayerUIManager playerUIManager;
    public GameObject missilePrefab;
    public GameObject rainMagicPrefab;
    public Transform leftControllerAnchor;
    public Transform rightControllerAnchor;


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        MP = maxMP;
        playerUIManager.Init(this);
        isDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            MissileAttack(rightControllerAnchor);
        }

        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            MissileAttack(leftControllerAnchor);
        }

        // START: PCデバッグ用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MissileAttack(rightControllerAnchor);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RainAttack();
        }
        // END: PCデバッグ用

        if (MP < maxMP)
        {
            MP += 0.5f;
            playerUIManager.MPSlider.value = MP;
        }

    }

    private void OnTriggerEnter(Collider other) {
        EnemyDamager damager = other.GetComponent<EnemyDamager>();
        if (damager)
        {
            GetDamage(damager.damage);
        }
    }

    public void GetDamage(int damage)
    {
        if (isDie)
        {
            return;
        }

        HP -= damage;
        playerUIManager.GetDamage(HP);

        if ( HP <= 0 )
        {
            HP = 0;
            Die();
        }
    }

    private void Die()
    {
        isDie = true;
        GameObject.Find("IngameSceneManager").GetComponent<IngameSceneManager>().isPlayerDie = isDie;
    }

    private void MissileAttack(Transform controllerAnchor)
    {
        if (MP >= 20)
        {
            MP -= 20.0f;
            playerUIManager.UpdateMP(MP);
            GameObject missileObject = Instantiate(missilePrefab, controllerAnchor.position, controllerAnchor.rotation) as GameObject;
            missileObject.GetComponent<Rigidbody>().AddForce(missileObject.transform.forward * 1000);
            Destroy(missileObject, 5.0f);
        }
    }

    private void RainAttack()
    {
        Instantiate(rainMagicPrefab, transform.position + new Vector3(0, -1.0f, 0), Quaternion.Euler(-90, 0, 0));
    }
}
