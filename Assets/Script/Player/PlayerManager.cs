using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 1000;
    int HP;
    public bool isDie;
    public PlayerUIManager playerUIManager;
    public GameObject magicPrefab;
    public GameObject rainMagicPrefab;
    public Transform rightControllerAnchor;


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        playerUIManager.Init(this);
        isDie = false;
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

    private void Attack()
    {
        GameObject magicGameObject = Instantiate(magicPrefab, rightControllerAnchor.position, rightControllerAnchor.rotation) as GameObject;
        magicGameObject.GetComponent<Rigidbody>().AddForce(magicGameObject.transform.forward * 1000);
    }

    private void RainAttack()
    {
        Instantiate(rainMagicPrefab, transform.position + new Vector3(0, -1.0f, 0), Quaternion.Euler(-90, 0, 0));
    }
}
